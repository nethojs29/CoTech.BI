using System;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Modules.Lender.Models{
    public class LenderEntity{
        public long Id{ set; get; }
        public string Name{ set; get; }
        public string RFC{ set; get; }
        public string Address{ set; get; }
        public string Suburb{ set; get; }
        public string postalCode{ set; get; }
        public string City{ set; get; }
        public string State{ set; get; }
        public string Phone{ set; get; }
        public string Email{ set; get; }
        public int Increment{ set; get; }
        public int Status{ set; get; }
        
        public long CompanyId{ set; get; }
        public CompanyEntity Company{ set; get; }
        public long CreatorId{ set; get; }
        public UserEntity Creator{ set; get; }
        
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
    }
}