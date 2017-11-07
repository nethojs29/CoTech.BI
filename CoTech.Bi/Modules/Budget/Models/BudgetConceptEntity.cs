using System;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Modules.Expenses.Models;

namespace CoTech.Bi.Modules.Budget.Models{
    public class BudgetConceptEntity{
        public long Id{ set; get; }
        
        public long BudgetId{ set; get; }
        
        public float Amount{ set; get; }
        
        public long CompanyId{ set; get; }
        
        public long CreatorId{ set; get; }
        
        public long ExpenseGroupId{ set; get; }
        public long ExpenseTypeId{ set; get; }
        
        public CompanyEntity Company{ set; get; }
        public ExpenseGroupEntity ExpenseGroup{ set; get; }
        public ExpenseTypeEntity ExpenseType{ set; get; }
        public BudgetEntity Budget{ set; get; }
        public UserEntity Creator{ set; get; }
        
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
    }
}