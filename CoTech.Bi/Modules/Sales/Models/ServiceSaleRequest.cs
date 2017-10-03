using System;

namespace CoTech.Bi.Modules.Sales.Models{
    public interface ServiceSaleRequest{}

    public class CreateServiceSaleReq : ServiceSaleRequest{
        public long ServiceId{ set; get; }
        public long ClientId{ set; get; }
        public float Price{ set; get; }
        public int Quantity{ set; get; }
        public DateTime Date{ set; get; }
        public bool iva{ set; get; }
        public long CompanyId{ set; get; }

        public ServiceSaleEntity toEntity(long userId){
            return new ServiceSaleEntity {
                ServiceId = ServiceId,
                ClientId = ClientId,
                Price = Price,
                Quantity = Quantity,
                Date = Date,
                iva = iva,
                CompanyId = CompanyId,
                CreatorId = userId,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateServiceSaleReq : ServiceSaleRequest{
        public long ServiceId{ set; get; }
        public long ClientId{ set; get; }
        public float Price{ set; get; }
        public int Quantity{ set; get; }
        public bool iva{ set; get; }
        public DateTime Date{ set; get; }
    }
}