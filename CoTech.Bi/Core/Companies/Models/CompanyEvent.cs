using System;

namespace CoTech.Bi.Core.Companies.Models
{
    public interface CompanyEvent {}
    public class CompanyCreatedEvt : CompanyEvent
    {
        public Guid Id { get; set; }
        public string Name {get; set;}
        public string Activity { get; set; }
        public string Url { get; set; }

        public CompanyCreatedEvt(CreateCompanyReq req) {
            Id = Guid.NewGuid();
            Name = req.Name;
            Activity = req.Activity;
            Url = req.Url;
        }
    }

    public class CompanyUpdatedEvt : CompanyEvent {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Activity { get; set;}
        public string Url { get; set; }

         public CompanyUpdatedEvt(UpdateCompanyReq req) {
            Id = Guid.NewGuid();
            Name = req.Name;
            Activity = req.Activity;
            Url = req.Url;
        }
    }

    public class CompanyDeletedEvt : CompanyEvent {
        public Guid Id { get; set; }
    }
}