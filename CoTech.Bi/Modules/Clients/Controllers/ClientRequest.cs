using System;
using CoTech.Bi.Modules.Clients.Models;

namespace CoTech.Bi.Modules.Clients.Controllers{
    public interface ClientRequest {}

    public class CreateClientReq : ClientRequest {
        public string Name{get;set;}
        public string Tradename{set; get;}
        public string RFC{set; get;}
        public string Address{set; get;}
        public string City{set; get;}
        public string State{set; get;}
        public string Email{set; get;}
        public string Phone{set; get;}
        public long CompanyId{set; get;}

        public ClientEntity toEntity(){
            return new ClientEntity{
                Name = Name,
                Tradename = Tradename,
                RFC = RFC,
                Address = Address,
                City = City,
                State = State,
                Phone = Phone,
                Email = Email,
                CompanyId = CompanyId,
                Status = 1,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateClientReq:ClientRequest{
        public string Name{set; get;}
        public string Tradename{set; get;}
        public string RFC{set; get;}
        public string Address{set; get;}
        public string City{set; get;}
        public string State{set; get;}
        public string Email{set; get;}
        public string Phone{set; get;}
        public int Status{set; get;}
    }
}