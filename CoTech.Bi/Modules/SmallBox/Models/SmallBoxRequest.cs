using System;

namespace CoTech.Bi.Modules.SmallBox.Models{
    public interface SmallBoxRequest{}

    public class CreateSmallBoxEntryReq : SmallBoxRequest{
        public string Concept{ set; get; }
        public float Amount{ set; get; }
        public DateTime Date{ set; get; }
        public int Type{ set; get; } // 0 egreso, 1 ingreso
        public long CompanyId{ set; get; }
        
        public long? ProviderId{ set; get; }
        public long? ClientId{ set; get; }

        public SmallBoxEntity toEntity(long userId){
            return new SmallBoxEntity {
                Concept = Concept,
                Amount = Amount,
                Date = Date,
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
        public DateTime Date{ set; get; }
        public int Type{ set; get; } // 0 egreso, 1 ingreso
        
        public long? ProviderId{ set; get; }
        public long? ClientId{ set; get; }
    }
}