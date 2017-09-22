
using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Companies.Models;

namespace CoTech.Bi.Modules.Providers.Models{
    public class ProviderEntity{
        public long Id{ set; get; }
        [Required]
        public string Name{ set; get; }
        [Required]
        public string Tradename{ set; get; }
        [Required]
        public string RFC{ set; get; }
        [Required]
        public string Address{ set; get; }
        [Required]
        public string Contact{ set; get; }
        [Required]
        public string Phone{ set; get; }
        [Required]
        public string Email{ set; get; }
        [Required]
        public long CompanyId{ set; get; }
        [Required]
        public CompanyEntity Company{ set; get; }
        [Required]
        public int Status{ set; get; }
        
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
        
    }
}