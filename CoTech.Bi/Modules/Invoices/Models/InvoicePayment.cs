using System;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Modules.Invoices.Models{
    public class InvoicePayment{
        public long Id{ set; get; }
        public double Payment{ set; get; }
        public DateTime Date{ set; get; }
        public string Observations{ set; get; }
        public string Folio{ set; get; }
        
        public long CompanyId{ set; get; }
        public long InvoiceId{ set; get; }
        public long CreatorId{ set; get; }
        
        public CompanyEntity Company{ set; get; }
        public InvoiceEntity Invoice{ set; get; }
        public UserEntity Creator{ set; get; }
        
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
    }
}