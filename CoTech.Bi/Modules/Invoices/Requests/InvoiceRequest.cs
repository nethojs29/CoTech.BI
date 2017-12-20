using System;
using CoTech.Bi.Modules.Invoices.Models;

namespace CoTech.Bi.Modules.Invoices.Requests
{
    public interface InvoiceRequest{}

    public class CreateInvoiceReq : InvoiceRequest{
        public float Date { set; get; }
        public string InvoiceCode { set; get; }
        public double Total { set; get; }
        public long ClientId { set; get; }
        public long BankId { set; get; }
        public string Observations { set; get; }
        public float StartPeriodDate { set; get; }
        public float EndPeriodDate { set; get; }
        
        public DateTime toDateTime(float date){
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(date).ToLocalTime();
        }

        public InvoiceEntity toEntity(long userId){
            return new InvoiceEntity{
                Date = toDateTime(Date),
                InvoiceCode = InvoiceCode,
                Total = Total,
                PaidAmount = 0,
                ClientId = ClientId,
                BankId = BankId,
                Observations = Observations,
                StartPeriodDate = toDateTime(StartPeriodDate),
                EndPeriodDate = toDateTime(EndPeriodDate),
                Status = 1,
                CreatedAt = DateTime.Now,
                CreatorId = userId
            };
        }
    }
}