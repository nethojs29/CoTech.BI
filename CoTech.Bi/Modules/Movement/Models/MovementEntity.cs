using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Companies.Models;

namespace CoTech.Bi.Modules.Movement.Models{
    public class MovementEntity{
        public long Id{ set; get; }
        [Required]
        public float Amount{ set; get; }
        [Required]
        public string Concept{ set; get; }
        [Required]
        public long CompanyId{ set; get; }
        public int Type{ set; get; } // 0 egreso, 1 ingreso
        
        public CompanyEntity Company{ set; get; }
        
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
       
    }
}