using System;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Modules.Banks.Models;
using CoTech.Bi.Modules.Clients.Models;

namespace CoTech.Bi.Modules.Invoices.Models
{
    public class InvoiceEntity
    {
        public long Id { set; get; }
        public DateTime Date { set; get; }
        public string InvoiceCode { set; get; }
        public double Total { set; get; }
        public double PaidAmount { set; get; }
        public long ClientId { set; get; }
        public long BankId { set; get; }
        public string Observations { set; get; }
        public DateTime StartPeriodDate { set; get; }
        public DateTime EndPeriodDate { set; get; }
        
        public int Status { set; get; } //1 Activa, 0 pagada (?
        
        public long CompanyId{ set; get; }
        
        public long CreatorId { set; get; }
        public DateTime CreatedAt { set; get; }
        public DateTime? DeletedAt { set; get; }
        
        public ClientEntity Client { set; get; }
        public BankEntity Bank { set; get; }
        public UserEntity Creator { set; get; }
        public CompanyEntity Company{ set; get; }
    }
}