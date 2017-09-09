using CoTech.Bi.Core.Notifications.Models;

namespace CoTech.Bi.Core.Notifications.Controllers
{
    public class NotificationResponse
    {
        public long Id { get; set; }
        public long SenderId { get; set; }
        public string Type { get; set; }
        public object Body { get; set; }

        public NotificationResponse(NotificationEntity entity){
            Id = entity.Id;
            SenderId = entity.SenderId;
            Type = entity.Type;
            Body = entity.Body;
        }
    }
}