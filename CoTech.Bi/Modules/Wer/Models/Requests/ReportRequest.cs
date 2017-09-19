using System;
using System.ComponentModel.DataAnnotations;

namespace CoTech.Bi.Modules.Wer.Models.Requests
{
    public class ReportRequest
    {
        [Required]
        public long UserId { set; get; }
        [Required]
        public long CompanyId { set; get; }
        [Required]
        public long WeekId { set; get; }
        
        
        public string Operative { set; get; }
        public string Financial { set; get; }
        public string Observation { set; get; }
    }
}