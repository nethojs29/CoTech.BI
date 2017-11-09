using System;
using CoTech.Bi.Modules.Movement.Models;

namespace CoTech.Bi.Modules.Movement.Controllers{
    public interface MovementRequest{}

    public class CreateMovementReq : MovementRequest{
        public float Amount{ set; get; }
        public string Concept{ set; get; }
        public float Date{ set; get; }
        public long CompanyId{ set; get; }
        public long ClientId{ set; get; }
        public int Type{ set; get; }
        public Boolean Iva{ set; get; }

        public MovementEntity toEntity(long userId){
            DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(Date).ToLocalTime();
            return new MovementEntity {
                Amount = Amount,
                Concept = Concept,
                CompanyId = CompanyId,
                Type = Type,
                Date = date,
                ClientId = ClientId,
                CreatorId = userId,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateMovementReq : MovementRequest{
        public float Amount{ set; get; }
        public string Concept{ set; get; }
        public int Type{ set; get; }
        public float Date{ set; get; }
        public long ClientId{ set; get; }
        public Boolean Iva{ set; get; }

        public DateTime getDate(){
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(Date).ToLocalTime();
        }
    }
}