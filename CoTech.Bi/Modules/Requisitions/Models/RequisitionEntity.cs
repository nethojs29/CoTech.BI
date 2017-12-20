using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Modules.Banks.Models;
using CoTech.Bi.Modules.DinningRooms.Models;
using CoTech.Bi.Modules.Lender.Models;

namespace CoTech.Bi.Modules.Requisitions.Models{
    public class RequisitionEntity{
        public long Id{ set; get; }
        
        [Required]
        public DateTime ApplicationDate{ set; get; }
        public string PaymentMethod{ set; get; }
        [Required]
        public long ResponsableId{ set; get; }
        [Required]
        public long CompanyId{ set; get; }
        [Required]
        public long CreatorId{ set; get; }
        [Required]
        public int Status{ set; get; }
        
        public long DinningRoomId{ set; get; }
        
        public float Total{ set; get; }
        public string Keyword{ set; get; }
        
        public long? ApproveUserId{ set; get; }
        public DateTime? ApproveDate{ set; get; }
        public long? LenderId{ set; get; }
        public string MotiveSurplus{ set; get; }
        
        public long? ComprobateUserId{ set; get; }
        public DateTime? ComprobateDate{ set; get; }
        public string ComprobateFileUrl{ set; get; }
        public float? Refund{ set; get; }
        
        public UserEntity Creator{ set; get; }
        public UserEntity Responsable{ set; get; }
        public UserEntity ApproveUser{ set; get; }
        public UserEntity ComprobateUSer{ set; get; }
        public LenderEntity Lender{ set; get; }
        public CompanyEntity Company{ set; get; }
        public DinningRoomEntity DinningRoom{ set; get; }
        
        [Required]
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
        
    }
}