using System;

namespace CoTech.Bi.Modules.Wer.Models.Responses
{
    public class LibraryResponse
    {
        public long Id { set; get; }
        public long ReportId { set; get; }
        public long WeekId { set; get; }
        public long UserId { set; get; }
        public int Type { set; get; }
        public string Name { set; get; }
        public string Mime { set; get; }
        public DateTime StartTime { set; get; }
        public DateTime EndTime { set; get; }
    }
}