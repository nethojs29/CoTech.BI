using System;
using CoTech.Bi.Modules.Lender.Models;

namespace CoTech.Bi.Modules.Lender.Controllers{
    public interface LenderRequest{}

    public class CreateLenderReq : LenderRequest{
        public string Name{ set; get; }
        public string RFC{ set; get; }
        public string Address{ set; get; }
        public string Suburb{ set; get; }
        public string postalCode{ set; get; }
        public string City{ set; get; }
        public string State{ set; get; }
        public string Phone{ set; get; }
        public string Email{ set; get; }
        public int Increment{ set; get; }
        public long CompanyId{ set; get; }

        public LenderEntity toEntity(long userId){
            return new LenderEntity {
                Name = Name,
                RFC = RFC,
                Address = Address,
                Suburb = Suburb,
                postalCode = postalCode,
                City = City,
                State = State,
                Phone = Phone,
                Email = Email,
                Increment = Increment,
                CompanyId = CompanyId,
                CreatorId = userId,
                Status = 1,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateLenderReq : LenderRequest{
        public string Name{ set; get; }
        public string RFC{ set; get; }
        public string Address{ set; get; }
        public string Suburb{ set; get; }
        public string postalCode{ set; get; }
        public string City{ set; get; }
        public string State{ set; get; }
        public string Phone{ set; get; }
        public string Email{ set; get; }
        public int Increment{ set; get; }
        public int Status{ set; get; }
    }
}