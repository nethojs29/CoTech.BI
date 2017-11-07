using System;
using CoTech.Bi.Modules.Personal.Models;

namespace CoTech.Bi.Modules.Personal.Controllers{
    public interface PersonalRequest{}

    public class CreatePersonalReq : PersonalRequest{
        public string Name{ set; get; }
        public string LastName{ set; get; }
        public string Email{ set; get; }
        public string Cellphone{ set; get; }
        public double StartDate{ set; get; }
        public string Position{ set; get; }
        public double DailyIMSSSalary{ set; get; }
        public double MonthlySalary{ set; get; }
        public string IMSSNumber{ set; get; }
        public string CivilState{ set; get; }
        public string RFC{ set; get; }
        public string CURP{ set; get; }
        public double BirthDate{ set; get; }
        public string BirthPlace{ set; get; }
        public string Address{ set; get; }
        public string Suburb{ set; get; }
        public string City{ set; get; }
        public string State{ set; get; }
        public string PostalCode{ set; get; }
        public string Phone{ set; get; }
        public string OtherContact{ set; get; }
        public int Infonavit{ set; get; }
        
        public long DepartmentId{ set; get; }
        public long? UserId{ set; get; }
        public long CompanyId{ set; get; }
        
        public PersonalEntity toEntity(long creatorId){
            Console.WriteLine("toEntity");
            Console.WriteLine(BirthDate);
            DateTime birthDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(BirthDate).ToLocalTime();
            DateTime startDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(StartDate).ToLocalTime();
//            startDate.AddMilliseconds(StartDate);
            Console.WriteLine(startDate);
            return new PersonalEntity {
                Name = Name,
                LastName = LastName,
                Email = Email,
                Cellphone = Cellphone,
                StartDate = startDate,
                Position = Position,
                DailyIMSSSalary = DailyIMSSSalary,
                MonthlySalary = MonthlySalary,
                IMSSNumber = IMSSNumber,
                CivilState = CivilState,
                RFC = RFC,
                CURP = CURP,
                BirthDate = birthDate,
                BirthPlace = BirthPlace,
                Address = Address,
                Suburb = Suburb,
                City = City,
                State = State,
                PostalCode = PostalCode,
                Phone = Phone,
                OtherContact = OtherContact,
                Infonavit = Infonavit,
                UserId = UserId,
                DepartmentId = DepartmentId,
                CompanyId = CompanyId,
                CreatorId = creatorId,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdatePersonalReq : PersonalRequest{
        public string Name{ set; get; }
        public string LastName{ set; get; }
        public string Email{ set; get; }
        public string Cellphone{ set; get; }
        public DateTime StartDate{ set; get; }
        public string Position{ set; get; }
        public double DailyIMSSSalary{ set; get; }
        public double MonthlySalary{ set; get; }
        public string IMSSNumber{ set; get; }
        public string CivilState{ set; get; }
        public string RFC{ set; get; }
        public string CURP{ set; get; }
        public DateTime BirthDate{ set; get; }
        public string BirthPlace{ set; get; }
        public string Address{ set; get; }
        public string Suburb{ set; get; }
        public string City{ set; get; }
        public string State{ set; get; }
        public string PostalCode{ set; get; }
        public string Phone{ set; get; }
        public string OtherContact{ set; get; }
        public int Infonavit{ set; get; }
        
        public long DepartmentId{ set; get; }
    }
}