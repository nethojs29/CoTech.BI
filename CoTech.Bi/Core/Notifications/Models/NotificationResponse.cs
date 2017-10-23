using System;
using System.Linq;
using CoTech.Bi.Core.Notifications.Models;

namespace CoTech.Bi.Core.Notifications.Models
{
    public class NotificationResponse
    {
        public long Id { get; set; }
        public long SenderId { get; set; }
        public string Type { get; set; }
        public object Body { get; set; }
        public bool Seen { get; set; }

        public NotificationResponse(NotificationEntity entity, long userId){
            Id = entity.Id;
            SenderId = entity.SenderId;
            Type = entity.Body.GetType().ToString();
            Body = entity.Body;
            Seen = entity.Receivers.First(r => r.UserId == userId).Read;
        }
    }
}