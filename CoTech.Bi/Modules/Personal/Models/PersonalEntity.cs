using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Modules.Personal.Models{
    public class PersonalEntity{
        public long Id{ set; get; }
        [Required]
        public string Name{ set; get; }
        [Required]
        public string LastName{ set; get; }
        [Required]
        public string Email{ set; get; }
        [Required]
        public string Cellphone{ set; get; }
        [Required]
        public DateTime StartDate{ set; get; }
        [Required]
        public string Position{ set; get; }
        [Required]
        public double DailyIMSSSalary{ set; get; }
        [Required]
        public double MonthlySalary{ set; get; }
        [Required]
        public string IMSSNumber{ set; get; }
        [Required]
        public string CivilState{ set; get; }
        [Required]
        public string RFC{ set; get; }
        [Required]
        public string CURP{ set; get; }
        [Required]
        public DateTime BirthDate{ set; get; }
        [Required]
        public string BirthPlace{ set; get; }
        [Required]
        public string Address{ set; get; }
        [Required]
        public string Suburb{ set; get; }
        [Required]
        public string City{ set; get; }
        [Required]
        public string State{ set; get; }
        [Required]
        public string PostalCode{ set; get; }
        [Required]
        public string Phone{ set; get; }
        [Required]
        public string OtherContact{ set; get; }
        [Required]
        public int Infonavit{ set; get; }
        
        public long DepartmentId{ set; get; }
        public DepartmentEntity Department{ set; get; }
        
        public long CompanyId{ set; get; }
        public CompanyEntity Company{ set; get; }
        
        public long CreatorId{ set; get; }
        public UserEntity Creator{ set; get; }
        
        public long? UserId{ set; get; }
        public UserEntity User{ set; get; }
        
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }

    }
    
}