using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Modules.Providers.Models;

namespace CoTech.Bi.Modules.Expenses.Models{
    public class ExpenseEntity{
        public long Id{ set; get; }
        [Required]
        public string Description{ set; get; }
        [Required]
        public float Price{ set; get; }
        [Required]
        public int Quantity{ set; get; }
        
        public string Observations{ set; get; }
        public string ImageUrl{ set; get; }
        
        public long ProviderId{ set; get; }
        public ProviderEntity Provider{ set; get; }
        public long ExpenseGroupId{ set; get; }
        public ExpenseGroupEntity Group{ set; get; }
        
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
    }
}