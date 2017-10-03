using System;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Modules.Wer.Models.Entities
{
    public class SeenMessagesEntity
    {
        public long Id { set; get; }
        public DateTime SeenAt { set; get; } = DateTime.Now;
        
        public long UserId { set; get; }
        public UserEntity User { set; get; }
        
        public long MessageId { set; get; }
        public MessageEntity Message { set; get; }
    }
}