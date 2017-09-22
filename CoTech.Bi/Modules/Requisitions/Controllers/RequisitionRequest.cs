using System;
using CoTech.Bi.Modules.Requisitions.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CoTech.Bi.Modules.Requisitions.Controllers{
    public interface RequisitionRequest{}

    public class CreateRequisitionReq : RequisitionRequest{
        public DateTime ApplicationDate{ set; get; }
        public string PaymentMethod{ set; get; }
        public long ResponsableId{ set; get; }
        public float Total{ set; get; }
        public long CompanyId{ set; get; }

        public RequisitionEntity toEntity(long creatorId){
            Console.WriteLine(creatorId);
            return new RequisitionEntity {
                ApplicationDate = ApplicationDate,
                PaymentMethod = PaymentMethod,
                ResponsableId = ResponsableId,
                Total = Total,
                CompanyId = CompanyId,
                CreatorId = creatorId,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateRequisitionReq : RequisitionRequest{
        public DateTime ApplicationDate{ set; get; }
        public string PaymentMethod{ set; get; }
        public long ResponsableId{ set; get; }
        public float Total{ set; get; }
        public int Status{ set; get; }

        public RequisitionEntity toEntity(){
            return new RequisitionEntity {
                ApplicationDate = ApplicationDate,
                PaymentMethod = PaymentMethod,
                ResponsableId = ResponsableId,
                Total = Total,
                Status = Status
            };
        }
    }

    public class ApproveRequisitionReq : RequisitionRequest{
        public string MotiveSurplus{ set; get; }
    }

    public class ComprobateRequisitionReq : RequisitionRequest{
        public DateTime ApplicationDate{ set; get; }
        public string PaymentMethod{ set; get; }
        public long ResponsableId{ set; get; }
        public float Total{ set; get; }
        public int Status{ set; get; }
        public long ComprobateUserId{ set; get; }
        public DateTime ComprobateDate = DateTime.Now;
        public string ComprobateFileUrl{ set; get; }
        public float Refund{ set; get; }
    }
}