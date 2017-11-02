using System;
using System.Collections.Generic;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Modules.Wer.Models.Entities
{
    public class MessageEntity
    {
        public long Id { set; get; }
        public string Message { set; get; }
        public DateTime CreatedAt { set; get; } = DateTime.Now;
        public string Tags { set; get; }
        
        public long UserId { set; get; }
        public UserEntity User { set; get; }
        
        public long WeekId { set; get; }
        public WeekEntity Week { set; get; }
        
        public long GroupId { set; get; }
        public GroupEntity Group { set; get; }
        
    }
}