namespace CoTech.Bi.Modules.Wer.Models.Entities
{
    public class FileEntity
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Mime { set; get; }
        public string Uri { set; get; }
        public int Type { set; get; }
        public string Extension { set; get; }
        
        public long ReportId { set; get; }
        public ReportEntity Report { set; get; }
    }
}