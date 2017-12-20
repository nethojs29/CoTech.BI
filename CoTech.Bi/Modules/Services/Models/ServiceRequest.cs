using System;

namespace CoTech.Bi.Modules.Services.Models{
    public interface ServiceRequest{}

    public class CreateServiceReq : ServiceRequest{
        public string Name{ set; get; }
        public long CompanyId{ set; get; }

        public ServiceEntity toEntity(long userId){
            return new ServiceEntity {
                Name = Name,
                CompanyId = CompanyId,
                CreatorId = userId,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateServiceReq : ServiceRequest{
        public string Name{ set; get; }
    }

    public class CreateServicePriceClientReq : ServiceRequest{
        public float Price{ set; get; }
        public long ClientId{ set; get; }
        public long ServiceId{ set; get; }
        public long CompanyId{ set; get; }

        public Service_Price_ClientEntity toEntity(long userId){
            return new Service_Price_ClientEntity {
                Price = Price,
                ClientId = ClientId,
                ServiceId = ServiceId,
                CompanyId = CompanyId,
                CreatorId = userId,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateServicePriceClientReq : ServiceRequest{
        public float Price{ set; get; }
    }
}