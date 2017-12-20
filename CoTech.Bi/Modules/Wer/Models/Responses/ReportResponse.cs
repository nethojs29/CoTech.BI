using System.Collections.Generic;
using System.Linq;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Core.Users.Repositories;
using CoTech.Bi.Modules.Wer.Models.Entities;

namespace CoTech.Bi.Modules.Wer.Models.Responses
{
    public class ReportResponse
    {
        public long Id { set; get; }
        public string Operative { set; get; }
        public string Financial { set; get; }
        public string Observation{ set; get; }
        public long UserId { set; get; }
        public long WeekId { set; get; }
        public long CompanyId { set; get; }
        public FileEntity[] Files { set; get; }
        
        public SeenReportsEntity[] Seen { set; get; }

        public ReportResponse(ReportEntity report)
        {
            this.Id = report.Id;
            this.CompanyId = report.CompanyId;
            this.Files = report.Files.Select(r => new FileEntity()
            {
                Id = r.Id,
                Extension = r.Extension,
                Mime = r.Mime,
                Name = r.Name,
                ReportId = r.ReportId,
                Uri = r.Uri,
                Type = r.Type
            }).ToArray();
            this.Seen = report.Seen.Select(r => new SeenReportsEntity()
            {
                Id = r.Id,
                ReportId = r.ReportId,
                SeenAt = r.SeenAt,
                UserId = r.UserId,
                User = r.User
            }).ToArray();
            this.Financial = report.Financial == null ? "": report.Financial;
            this.Observation = report.Observation == null ? "": report.Observation;
            this.Operative = report.Operative == null ? "": report.Operative;
            UserId = report.UserId;
            this.WeekId = report.WeekId;
        }
    }
}