
using System;
using CoTech.Bi.Modules.Providers.Models;

namespace CoTech.Bi.Modules.Providers.Controllers{
    public interface ProviderRequest{}

    public class CreateProviderReq : ProviderRequest{
        public string Name{ set; get; }
        public string Tradename{ set; get; }
        public string RFC{ set; get; }
        public string Address{ set; get; }
        public string Contact{ set; get; }
        public string Phone{ set; get; }
        public string Email{ set; get; }
        public long CompanyId{ set; get; }

        public ProviderEntity toEntity(long userId){
            return new ProviderEntity {
                Name = Name,
                Tradename = Tradename,
                RFC = RFC,
                Address = Address,
                Contact = Contact,
                Phone = Phone,
                Email = Email,
                CompanyId = CompanyId,
                Status = 1,
                CreatorId = userId,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateProviderReq : ProviderRequest{
        public string Name{ set; get; }
        public string Tradename{ set; get; }
        public string RFC{ set; get; }
        public string Address{ set; get; }
        public string Contact{ set; get; }
        public string Phone{ set; get; }
        public string Email{ set; get; }
        public int Status{ set; get; }
    }
}