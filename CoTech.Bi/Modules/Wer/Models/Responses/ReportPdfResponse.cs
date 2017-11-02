using System;

namespace CoTech.Bi.Modules.Wer.Models.Responses
{
    public class ReportPdfResponse
    {
        public string User { set; get; }
        public string Company { set; get; }
        public string Color { set; get; }
        public DateTime StarTime { set; get; }
        public DateTime EndTime { set; get; }
        public string Operative { set; get; }
        public string Finantial { set; get; }
        public string Observations { set; get; }
    }
}