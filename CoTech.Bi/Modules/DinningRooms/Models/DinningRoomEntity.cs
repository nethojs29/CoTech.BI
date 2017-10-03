using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Modules.DinningRooms.Models{
    public class DinningRoomEntity{
        public long Id{ set; get; }
        [Required]
        public string Name{ set; get; }
        public string Address{ set; get; }
        public string City{ set; get; }
        public string State{ set; get; }
        public string Responsable{ set; get; }
        public string Phone{ set; get; }
        public string Email{ set; get; }
        
        public int Status{ set; get; }
        
        public long CompanyId{ set; get; }
        public CompanyEntity Company{ set; get; }
        
        public long UserId{ set; get; }
        public UserEntity User{ set; get; }
        
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
        
    }
}