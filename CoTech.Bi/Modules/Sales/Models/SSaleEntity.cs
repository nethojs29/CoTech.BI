using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Modules.Clients.Models;

namespace CoTech.Bi.Modules.Sales.Models{
    public class SSaleEntity{
        public long Id{ set; get; }
        [Required]
        public double Total{ set; get; }
        [Required]
        public DateTime Date{ set; get; }
        
        public long ClientId{ set; get; }
        public ClientEntity Client{ set; get; }
        
        public long CompanyId{ set; get; }
        public CompanyEntity Company{ set; get; }
        
        public long CreatorId{ set; get; }
        public UserEntity Creator{ set; get; }
        
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
    }
}