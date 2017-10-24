using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Wer.Models.Entities;
using CoTech.Bi.Modules.Wer.Models.Requests;
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

        public ReplyController(ReplyRepository replyRepository)
        {
            this._replyRepository = replyRepository;
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
                if (values != null)
                {
                    return BadRequest(new {message = "Imposible crear con datos especificados."});
                }
                return new ObjectResult(message){StatusCode = 201};
            }
            catch (Exception e)
            {
                return new ObjectResult(new {messsage = e.Message}){StatusCode = 500};
            }
        }
        // GET
        [HttpGet("reply/mine")]
        [RequiresRole(WerRoles.Ceo,WerRoles.Director,WerRoles.Operator)]
        public async Task GetMyMessages()
        {
            if(HttpContext.WebSockets.IsWebSocketRequest){
                var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                var userId = HttpContext.UserId().Value;
                var messagesObs = _replyRepository.UserMessages(userId);
                var sender = new MessagesSender(socket);
                messagesObs.Subscribe(sender);
                await sender.UntilWebsocketCloses();
            } else {
                HttpContext.Response.StatusCode = 400;
            }
        }
    }
    public class MessagesSender : IObserver<MessageEntity>
    {
        private WebSocket webSocket;
        public MessagesSender(WebSocket socket){
            this.webSocket = socket;
        }
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            Console.Error.WriteLine(error);
        }

        public void OnNext(MessageEntity value)
        {
            SendNext(value).Wait(1000);
        }

        public async Task SendNext(MessageEntity entity){
            if(webSocket.State == WebSocketState.Open){
                
                var json = JsonConvert.SerializeObject(entity, JsonConverterOptions.JsonSettings);
                var bytes = Encoding.Unicode.GetBytes(json);
                var segment = new ArraySegment<byte>(bytes);
                await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public async Task UntilWebsocketCloses() {
            var bytes = new byte[1024*4];
            do {
                var res = await webSocket.ReceiveAsync(new ArraySegment<byte>(bytes), CancellationToken.None);
                if(res.MessageType == WebSocketMessageType.Close) break;
            } while (webSocket.State == WebSocketState.Open);
        }

    }
}