using System;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Modules.Wer.Models.Entities
{
    public class PartyEntity
    {
        public long Id { set; get; }
        public DateTime DateIn { set; get; } = DateTime.Now;
        public DateTime? DateOut { set; get; } = null;
        public DateTime? Createdat { set; get; } = DateTime.Now;
        
        public long UserId { set; get; }
        public UserEntity User { set; get; }
        
        public long GroupId { set; get; }
        public GroupEntity Group { set; get; }
    }
}