using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Modules.Clients.Models;

namespace CoTech.Bi.Modules.Movement.Models{
    public class MovementEntity{
        public long Id{ set; get; }
        [Required]
        public float Amount{ set; get; }
        [Required]
        public string Concept{ set; get; }
        [Required]
        public long CompanyId{ set; get; }
        [Required]
        public long ClientId{ set; get; }
        public int Type{ set; get; } // 0 egreso, 1 ingreso
        
        
        public CompanyEntity Company{ set; get; }
        public ClientEntity Client{ set; get; }
        
        public long CreatorId{ set; get; }
        public UserEntity Creator{ set; get; }
        
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
       
    }
}