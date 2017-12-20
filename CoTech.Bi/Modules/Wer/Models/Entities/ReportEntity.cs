using  System;
using System.Collections.Generic;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Modules.Wer.Models.Entities
{
    public class ReportEntity
    {
        public long Id { set; get; }
        public string Operative { set; get; }
        public string Financial { set; get; }
        public string Observation { set; get; }
        public DateTime Updated { set; get; } = DateTime.Now;
        
        public long UserId { set; get; }
        public UserEntity User { set; get; }

        
        public long WeekId { set; get; }
        public WeekEntity Week { set; get; }
        
        public long CompanyId { set; get; }
        public CompanyEntity Company { set; get; }
        
        public List<FileEntity> Files { set; get; }
        
        public List<SeenReportsEntity> Seen { set; get; }
    }
}