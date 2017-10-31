using System;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Modules.Budget.Models{
    public class BudgetConceptEntity{
        public long Id{ set; get; }
        
        public long BudgetId{ set; get; }
        
        public float Amount{ set; get; }
        
        public long CompanyId{ set; get; }
        
        public long CreatorId{ set; get; }
        
        public CompanyEntity Company{ set; get; }
        public BudgetEntity Budget{ set; get; }
        public UserEntity Creator{ set; get; }
        
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
    }
}