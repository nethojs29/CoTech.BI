using System;
using CoTech.Bi.Core.Notifications.Models;

namespace CoTech.Bi.Core.Notifications.Models
{
    public class NotificationResponse
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public object Body { get; set; }

        public NotificationResponse(NotificationEntity entity){
            Id = entity.Id;
            SenderId = entity.SenderId;
            Body = entity.Body;
        }
    }
}