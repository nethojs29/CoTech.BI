using System;
using CoTech.Bi.Modules.Requisitions.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CoTech.Bi.Modules.Requisitions.Controllers{
    public interface RequisitionRequest{}

    public class CreateRequisitionReq : RequisitionRequest{
        public long ResponsableId{ set; get; }
        public long CompanyId{ set; get; }
        public long? LenderId{ set; get; }
        public long? BankId{ set; get; }

        public RequisitionEntity toEntity(long creatorId){
            Console.WriteLine(creatorId);
            return new RequisitionEntity {
                ApplicationDate = DateTime.Now,
                ResponsableId = ResponsableId,
                CompanyId = CompanyId,
                CreatorId = creatorId,
                CreatedAt = DateTime.Now,
                Status = 1,
                LenderId = LenderId,
                BankId = BankId
            };
        }
    }

    public class UpdateRequisitionReq : RequisitionRequest{
        public DateTime ApplicationDate{ set; get; }
        public string PaymentMethod{ set; get; }
        public long ResponsableId{ set; get; }
        public int Status{ set; get; }

        public RequisitionEntity toEntity(){
            return new RequisitionEntity {
                ApplicationDate = ApplicationDate,
                PaymentMethod = PaymentMethod,
                ResponsableId = ResponsableId,
                Status = Status
            };
        }
    }

    public class ApproveRequisitionReq : RequisitionRequest{
        public string MotiveSurplus{ set; get; }
    }

    public class ComprobateRequisitionReq : RequisitionRequest{
        public long ComprobateUserId{ set; get; }
        public DateTime ComprobateDate = DateTime.Now;
        public string ComprobateFileUrl{ set; get; }
        public float Refund{ set; get; }
    }
}