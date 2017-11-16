using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Modules.Providers.Models;
using CoTech.Bi.Modules.Requisitions.Models;

namespace CoTech.Bi.Modules.Expenses.Models{
    public class ExpenseEntity{
        public long Id{ set; get; }
        [Required]
        public float Price{ set; get; }
        [Required]
        public int Quantity{ set; get; }
        
        public string Observations{ set; get; }
        
        public long RequisitionId{ set; get; }
        public RequisitionEntity Requisition{ set; get; }
        
        public long ProviderId{ set; get; }
        public ProviderEntity Provider{ set; get; }
        [Required]
        public long ExpenseGroupId{ set; get; }
        public ExpenseGroupEntity ExpenseGroup{ set; get; }
        public long CompanyId{ set; get; }
        public CompanyEntity Company{ set; get; }
        
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
    }
}