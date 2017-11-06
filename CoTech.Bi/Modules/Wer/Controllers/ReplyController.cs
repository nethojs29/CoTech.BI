using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Wer.Models.Entities;
using CoTech.Bi.Modules.Wer.Models.Requests;
using CoTech.Bi.Modules.Wer.Models.Responses;
using CoTech.Bi.Modules.Wer.Repositories;
using CoTech.Bi.Util;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoTech.Bi.Modules.Wer.Controllers
{
    [Route("/api/companies/{idCompany}/res")]
    public class ReplyController : Controller
    {

        private ReplyRepository _replyRepository;
        private NotificationsIOSRepository _notifications;

        public ReplyController(ReplyRepository replyRepository, NotificationsIOSRepository _notifications)
        {
            this._replyRepository = replyRepository;
            this._notifications = _notifications;
        }

        [HttpGet("groups")]
        [RequiresAuth]
        public async Task<IActionResult> GetGroups()
        {
            try
            {
                var groups = await _replyRepository.SearchGroups(HttpContext.UserId().Value);
                return new OkObjectResult(groups);
            }
            catch (Exception e)
            {
                return new ObjectResult(new {message = e.Message}){StatusCode = 500};
            }
        }

        [HttpGet("groups/{idGroup}")]
        [RequiresRole(WerRoles.Ceo, WerRoles.Director, WerRoles.Operator)]
        public async Task<IActionResult> GetMessages([FromRoute] long idCompany,[FromRoute] long idGroup,
            long idMessage = 0, int count = 30)
        {
            try
            {
                var group = _replyRepository.GetGroup(idGroup);
                if (group != null)
                {
                    group.messages = await _replyRepository.GetMessage(HttpContext.UserId().Value, idGroup, idMessage, count);
                    return new ObjectResult(group)
                    {
                        StatusCode = 200
                    };
                }
            }
            catch (Exception e)
            {
                return new ObjectResult(new {message = e.Message}){StatusCode = 500};
            }
        }

        [HttpGet("reply/{type}/user/{idUser}")]
        [RequiresRole(WerRoles.Ceo, WerRoles.Director, WerRoles.Operator)]
        public async Task<IActionResult> UpdateParty([FromRoute] long idCompany, [FromRoute] int type,
            [FromRoute] long idUser)
        {
            try
            {
                var creator = HttpContext.UserId().Value;
                var partyFound = _replyRepository.FindParty(idCompany, idUser, creator, type);
                if (partyFound != null)    
                {
                    var message = _replyRepository.messagesNotView(creator, partyFound.GroupId);
                    var party = _replyRepository.UpdateParty(idCompany, idUser, creator, type);
                    if (message != null)
                    {
                        return new OkObjectResult(
                            new GroupResponse(party.Group, message)
                        );
                    }
                    return new OkObjectResult(
                        new GroupResponse(party.Group)
                    );
                }
                return new ObjectResult(new {message = "no se encontro grupo de chat"}){StatusCode = 404};
            }
            catch (Exception e)
            {
                return new ObjectResult(new {message = e.Message}){StatusCode = 500};

            }
        }

        [HttpPost("reply/{type}/user/{idUser}")]
        [RequiresRole(WerRoles.Ceo,WerRoles.Director,WerRoles.Operator)]
        public async Task<IActionResult> CreateReply([FromRoute]long idCompany,[FromRoute] int type,[FromRoute] long idUser,[FromBody] MessageRequest message)
        {
            try
            {
                var creator = HttpContext.UserId().Value;
                var values =
                    await _replyRepository.SearchOrCreateGroup(idCompany, idUser, creator, type,
                        new MessageEntity()
                        {
                            Message = message.Message,
                            Tags = message.Tags,
                            UserId = creator,
                            WeekId = message.WeekId
                        });
                if (values == null)
                {
                    return BadRequest(new {message = "Imposible crear con datos especificados."});
                }
                var response = new MessageResponse(values);
                string messageString = values.User.Name + " " + values.User.Lastname + ": " + response.Message; 
                var userNotify = new List<long>(){idUser,creator};
                _notifications.SendNotification(userNotify,messageString,response);
                return new ObjectResult(response){StatusCode = 201};
            }
            catch (Exception e)
            {
                return new ObjectResult(new {messsage = e.Message}){StatusCode = 500};
            }
        }
        // GET
        [HttpGet("reply/mine")]
        [RequiresAuth]
        public async Task GetMyMessages()
        {
            try
            {
                if (HttpContext.WebSockets.IsWebSocketRequest)
                {
                    var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                    var userId = HttpContext.UserId().Value;
                    var messagesObs = _replyRepository.UserMessages(userId);
                    var sender = new MessagesSender(socket, userId);
                    messagesObs.Subscribe(sender);
                    await sender.UntilWebsocketCloses();
                }
                else
                {
                    HttpContext.Response.StatusCode = 400;
                }
            }
            catch (Exception e)
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(e.Message);
                MemoryStream stream = new MemoryStream(byteArray);
                HttpContext.Response.Body = stream;
                HttpContext.Response.StatusCode = 500;
            }
        }
    }
    public class MessagesSender : IObserver<MessageEntity>
    {
        private WebSocket webSocket;
        private long userId;
        public MessagesSender(WebSocket socket,long idUser){
            this.webSocket = socket;
            this.userId = idUser;
        }
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            try
            {
                Console.WriteLine("\n\n Error Socket");
                Console.Error.WriteLine(error);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void OnNext(MessageEntity value)
        {
            try
            {
                SendNext(value).Wait(1000);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\n CATCH OnNext");
                Console.WriteLine(e);
            }
        }

        private async Task SendNext(MessageEntity entity){
            try
            {
                if(webSocket.State == WebSocketState.Open){
                    var res = new MessageResponse(entity);
                    var json = JsonConvert.SerializeObject(res, JsonConverterOptions.JsonSettings);
                    var bytes = Encoding.UTF8.GetBytes(json);
                    var segment = new ArraySegment<byte>(bytes);
                    await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\n CATCH SendNext");
                Console.WriteLine(e);
            }
        }

        public async Task UntilWebsocketCloses() {
            try
            {
                var bytes = new byte[1024*4];
                do {
                    var res = await webSocket.ReceiveAsync(new ArraySegment<byte>(bytes), CancellationToken.None);
                    if(res.MessageType == WebSocketMessageType.Close) break;
                } while (webSocket.State == WebSocketState.Open);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\n CATCH UtilNext");
                Console.WriteLine(e);
            }
        }

    }
}