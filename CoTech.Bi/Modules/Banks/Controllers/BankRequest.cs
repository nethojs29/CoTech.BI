using System;
using CoTech.Bi.Modules.Banks.Models;

namespace CoTech.Bi.Modules.Clients.Controllers{
    public interface BankRequest{}

    public class CreateBankReq : BankRequest{
        public string Name{ set; get; }
        public string RFC{ set; get; }
        public string Bank{ set; get; }
        public string Account{ set; get; }
        public long CompanyId{set; get;}

        public BankEntity toEntity(long userId){
            return new BankEntity {
                Name = Name,
                RFC = RFC,
                Bank = Bank,
                Account = Account,
                Status = 1,
                CompanyId = CompanyId,
                CreatorId = userId,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateBankReq : BankRequest{
        public string Name{ set; get; }
        public string RFC{ set; get; }
        public string Bank{ set; get; }
        public string Account{ set; get; }
        public int Status{ set; get; }
    }
}