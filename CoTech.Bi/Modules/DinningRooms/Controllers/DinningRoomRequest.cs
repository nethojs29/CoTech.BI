using System;
using CoTech.Bi.Modules.DinningRooms.Models;

namespace CoTech.Bi.Modules.DinningRooms.Controllers{
    public interface DinningRoomRequest{
    }

    public class CreateDinningRoomReq : DinningRoomRequest{
        public string Name{ set; get; }
        public string Address{ set; get; }
        public string City{ set; get; }
        public string State{ set; get; }
        public string Responsable{ set; get; }
        public string Phone{ set; get; }
        public string Email{ set; get; }
        public long CompanyId{ set; get; }

        public DinningRoomEntity toEntity(long userId){
            return new DinningRoomEntity {
                Name = Name,
                Address = Address,
                City = City,
                State = State,
                Responsable = Responsable,
                Phone = Phone,
                Email = Email,
                CompanyId = CompanyId,
                UserId = userId,
                Status = 1,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateDinningRoomReq : DinningRoomRequest{
        public string Name{ set; get; }
        public string Address{ set; get; }
        public string City{ set; get; }
        public string State{ set; get; }
        public string Responsable{ set; get; }
        public string Phone{ set; get; }
        public string Email{ set; get; }
        public int Status{ set; get; }
    }

}