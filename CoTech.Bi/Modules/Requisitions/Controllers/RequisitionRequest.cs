using System;
using CoTech.Bi.Modules.Requisitions.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CoTech.Bi.Modules.Requisitions.Controllers{
    public interface RequisitionRequest{}

    public class CreateRequisitionReq : RequisitionRequest{
        public long ResponsableId{ set; get; }
        public long CompanyId{ set; get; }
        public float ApplicationDate{ set; get; }
        public long DinningRoomId{ set; get; }

        public RequisitionEntity toEntity(long creatorId){
            DateTime appDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(ApplicationDate).ToLocalTime();
            return new RequisitionEntity {
                ApplicationDate = appDate,
                PaymentMethod = "",
                ResponsableId = ResponsableId,
                DinningRoomId = DinningRoomId,
                CompanyId = CompanyId,
                CreatorId = creatorId,
                CreatedAt = DateTime.Now,
                Total = 0,
                Status = 1 //1 Pendiente, 2 Aprobada, 3 Comprobada, 0 Denegada
            };
        }
    }

    public class UpdateRequisitionReq : RequisitionRequest{
        public float ApplicationDate{ set; get; }
        public string PaymentMethod{ set; get; }
        public long ResponsableId{ set; get; }
        public int Status{ set; get; }
        public long DinningRoomId{ set; get; }
        public float Total{ set; get; }

        public DateTime getDate(){
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(ApplicationDate).ToLocalTime();
        }
    }

    public class ApproveRequisitionReq : RequisitionRequest{
        public string PaymentMethod{ set; get; }
        public long BankId{ set; get; }
    }

    public class ComprobateRequisitionReq : RequisitionRequest{
        public float Refund{ set; get; }
    }
}