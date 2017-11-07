using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Modules.Services.Models;

namespace CoTech.Bi.Modules.Sales.Models{
    public class ServiceSaleEntity{
        public long Id{ set; get; }
        [Required]
        public long ServiceId{ set; get; }
        [Required]
        public float Price{ set; get; }
        [Required]
        public int Quantity{ set; get; }
        [Required]
        public long CompanyId{ set; get; }
        
        public bool iva{ set; get; }
        
        public long SaleId{ set; get; }
        public SSaleEntity Sale{ set; get; }
        
        public CompanyEntity Company{ set; get; }
        public ServiceEntity Service{ set; get; }
        
        public long CreatorId{ set; get; }
        public UserEntity Creator{ set; get; }
        
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
    }
}