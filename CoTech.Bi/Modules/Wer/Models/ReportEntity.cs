using  System;
using CoTech.Bi.Core.Companies.Models;

namespace CoTech.Bi.Modules.Wer.Models
{
    public class ReportEntity
    {
        public long Id { set; get; }
        public long User { set; get; }
        public string Operative { set; get; }
        public string Financial { set; get; }
        public string Observation{ set; get; }
        
        public long WeekId { set; get; }
        public WeekEntity Week { set; get; }
        
        public long CompanyId { set; get; }
        public CompanyEntity Company { set; get; }
    }
}