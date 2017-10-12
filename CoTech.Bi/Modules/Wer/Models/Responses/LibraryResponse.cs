using System;

namespace CoTech.Bi.Modules.Wer.Models.Responses
{
    public class LibraryResponse
    {
        public long IdFile { set; get; }
        public long IdFormat { set; get; }
        public long IdWeek { set; get; }
        public long IdUser { set; get; }
        public int Type { set; get; }
        public string Name { set; get; }
        public string Mime { set; get; }
        public DateTime StartTime { set; get; }
        public DateTime EndTime { set; get; }
    }
}