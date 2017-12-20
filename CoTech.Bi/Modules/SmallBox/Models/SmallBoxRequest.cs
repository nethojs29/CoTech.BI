using System;

namespace CoTech.Bi.Modules.SmallBox.Models{
    public interface SmallBoxRequest{}

    public class CreateSmallBoxEntryReq : SmallBoxRequest{
        public string Concept{ set; get; }
        public float Amount{ set; get; }
        public float Date{ set; get; }
        public int Type{ set; get; } // 0 egreso, 1 ingreso
        public long CompanyId{ set; get; }
        public long BankId { set; get; }
        
        public long? ProviderId{ set; get; }
        public long? ClientId{ set; get; }

        public SmallBoxEntity toEntity(long userId){
            DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(Date).ToLocalTime();
            Console.WriteLine(ProviderId);
            Console.WriteLine(ClientId);
            return new SmallBoxEntity {
                Concept = Concept,
                Amount = Amount,
                Date = date,
                BankId = BankId,
                Type = Type,
                CompanyId = CompanyId,
                ProviderId = ProviderId,
                ClientId = ClientId,
                CreatorId = userId,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateSmallBoxEntryReq : SmallBoxRequest{
        public string Concept{ set; get; }
        public float Amount{ set; get; }
        public float Date{ set; get; }
        public int Type{ set; get; } // 0 egreso, 1 ingreso
        
        public long? ProviderId{ set; get; }
        public long? ClientId{ set; get; }
        
        public DateTime getDate(){
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(Date).ToLocalTime();
        }
    }
}