using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Modules.Requisitions.Models{
    public class RequisitionEntity{
        public long Id{ set; get; }
        
        [Required]
        public DateTime ApplicationDate{ set; get; }
        [Required]
        public string PaymentMethod{ set; get; }
        [Required]
        public long ResponsableId{ set; get; }
        [Required]
        public float Total{ set; get; }
        [Required]
        public long CompanyId{ set; get; }
        [Required]
        public long CreatorId{ set; get; }
        [Required]
        public int Status{ set; get; }
        
        public long? ApproveUserId{ set; get; }
        public DateTime ApproveDate{ set; get; }
        public string MotiveSurplus{ set; get; }
        
        public long? ComprobateUserId{ set; get; }
        public DateTime ComprobateDate{ set; get; }
        public string ComprobateFileUrl{ set; get; }
        public float Refund{ set; get; }
        
        public UserEntity Creator{ set; get; }
        public UserEntity Responsable{ set; get; }
        public UserEntity ApproveUser{ set; get; }
        public UserEntity ComprobateUSer{ set; get; }
        public CompanyEntity Company{ set; get; }
        
        [Required]
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
        
    }
}