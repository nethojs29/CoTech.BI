using System;
using CoTech.Bi.Modules.Movement.Models;

namespace CoTech.Bi.Modules.Movement.Controllers{
    public interface MovementRequest{}

    public class CreateMovementReq : MovementRequest{
        public float Amount{ set; get; }
        public string Concept{ set; get; }
        public long CompanyId{ set; get; }
        public int Type{ set; get; }

        public MovementEntity toEntity(long userId){
            return new MovementEntity {
                Amount = Amount,
                Concept = Concept,
                CompanyId = CompanyId,
                Type = Type,
                CreatorId = userId,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateMovementReq : MovementRequest{
        public float Amount{ set; get; }
        public string Concept{ set; get; }
        public int Type{ set; get; }
    }
}