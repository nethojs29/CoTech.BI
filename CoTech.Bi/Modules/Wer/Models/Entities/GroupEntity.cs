using System.Collections.Generic;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Modules.Wer.Models.Entities
{
    public class GroupEntity
    {
        public long Id { set; get; }
        public int Category { set; get; }
        
        public long UserId { set; get; }
        public UserEntity User { set; get; }
        
        public long CompanyId { set; get; }
        public CompanyEntity Company { set; get; }

        public List<PartyEntity> UsersList { set; get; }
        
        public List<MessageEntity> MessagesList { set; get; }
        
    }
}