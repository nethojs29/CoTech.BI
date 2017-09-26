using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Companies.Models;

namespace CoTech.Bi.Modules.Services.Models{
    public class ServiceEntity{
        public long Id{ set; get; }
        [Required]
        public string Name{ set; get; }
        [Required]
        public long CompanyId{ set; get; }
        
        public CompanyEntity Company{ set; get;}
        
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
    }
}