using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Modules.Clients.Models;
using CoTech.Bi.Modules.Providers.Models;

namespace CoTech.Bi.Modules.SmallBox.Models{
    public class SmallBoxEntity{
        public long Id{ set; get; }
        [Required]
        public string Concept{ set; get; }
        [Required]
        public float Amount{ set; get; }
        [Required]
        public DateTime Date{ set; get; }
        [Required]
        public int Type{ set; get; } // 0 egreso, 1 ingreso
        [Required]
        public long CompanyId{ set; get; }
        
        public long? ProviderId{ set; get; }
        public long? ClientId{ set; get; }
        
        public CompanyEntity Company{ set; get; }
        public ProviderEntity Provider{ set; get; }
        public ClientEntity Client{ set; get; }
        
        public long CreatorId{ set; get; }
        public UserEntity Creator{ set; get; }
        
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
    }
}