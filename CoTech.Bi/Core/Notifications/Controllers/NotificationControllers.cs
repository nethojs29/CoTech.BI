using System;
using System.Net.WebSockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Core.Notifications.Models;
using CoTech.Bi.Rx;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoTech.Bi.Core.Notifications.Controllers
{
    [Route("/api/notifications")]
    public class NotificationController : Controller
    {
        private EventEmitter emitter;
        private NotificationRepository notificationRepository;
        public NotificationController(NotificationRepository notificationRepository){
            this.notificationRepository = notificationRepository;
        }

        [HttpGet("mine")]
        public async Task GetMyNotifications(){
            if(HttpContext.WebSockets.IsWebSocketRequest){
                var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                var userId = HttpContext.UserId();
                var notificationObs = notificationRepository.UserNotifications(userId);
                var sender = new NotificationSender(socket);
                notificationObs.Subscribe(sender);
                await sender.UntilWebsocketCloses();
            } else {
              HttpContext.Response.StatusCode = 400;
            }
        }
    }

    public class NotificationSender : IObserver<NotificationEntity>
    {

        private WebSocket webSocket;
        public NotificationSender(WebSocket socket){
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

        public void OnNext(NotificationEntity value)
        {
            SendNext(value).Start();
        }

        public async Task SendNext(NotificationEntity entity){
            Console.WriteLine($"notification {entity}");
          if(webSocket.State == WebSocketState.Open){
              var json = JsonConvert.SerializeObject(entity);
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