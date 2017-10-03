using System;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Modules.Wer.Models.Entities
{
    public class SeenReportsEntity
    {
        public long Id { set; get; }
        public DateTime SeenAt { set; get; } = DateTime.Now;
        
        public long UserId { set; get; }
        public UserEntity User { set; get; }
        
        public long ReportId { set; get; }
        public ReportEntity Report { set; get; }
    }
}