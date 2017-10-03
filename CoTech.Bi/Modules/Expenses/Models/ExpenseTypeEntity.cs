using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Companies.Models;

namespace CoTech.Bi.Modules.Expenses.Models{
    public class ExpenseTypeEntity{
        public long Id{ set; get; }
        [Required]
        public string Name{ set; get; }
        [Required]
        public int CompanyId{ set; get; }
        public CompanyEntity Company{ set; get; }
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
    }
}