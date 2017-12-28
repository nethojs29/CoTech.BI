using System;
using CoTech.Bi.Modules.Invoices.Models;

namespace CoTech.Bi.Modules.Invoices.Requests{
    public interface InvoicePaymentRequest{}

    public class CreateInvoicePaymentReq : InvoicePaymentRequest{
        public double Payment{ set; get; }
        public float Date{ set; get; }
        public string Observations{ set; get; }
        public string Folio{ set; get; }
        public long CompanyId{ set; get; }
        public long InvoiceId{ set; get; }

        public DateTime toDateTime(float date){
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(date).ToLocalTime();
        }

        public InvoicePayment toEntity(long userId){
            return new InvoicePayment{
                Payment = Payment,
                Date = toDateTime(Date),
                Observations = Observations,
                Folio = Folio,
                CompanyId = CompanyId,
                InvoiceId = InvoiceId,
                CreatorId = userId,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateInvoicePaymentReq : InvoicePaymentRequest{
        public double Payment{ set; get; }
        public float Date{ set; get; }
        public string Observations{ set; get; }
        public string Folio{ set; get; }
        public long CompanyId{ set; get; }
        public long InvoiceId{ set; get; }
    }
}