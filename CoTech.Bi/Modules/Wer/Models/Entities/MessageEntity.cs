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
        
        public long UserId { set; get; }
        public UserEntity User { set; get; }
        
        public long GroupId { set; get; }
        public GroupEntity Group { set; get; }
        
        public List<SeenMessagesEntity> Seen { set; get; }
    }
}