using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Modules.Expenses.Models;

namespace CoTech.Bi.Modules.Budget.Models{
    public class BudgetEntity{
        public long Id{ set; get; }
        [Required]
        public int Month{ set; get; }
        [Required]
        public int Year{ set; get; }
        [Required]
        public float Amount{ set; get; }
        [Required]
        public long ExpenseTypeId{ set; get; }
        [Required]
        public long CompanyId{ set; get; }
        
        public long CreatorId{ set; get; }
        
        public int Type{ set; get; } //0 gasto, 1 venta    ???????
        
        public CompanyEntity Company{ set; get; }
        public ExpenseTypeEntity ExpenseType{ set; get; }
        public UserEntity Creator{ set; get; }
        
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
    }
}