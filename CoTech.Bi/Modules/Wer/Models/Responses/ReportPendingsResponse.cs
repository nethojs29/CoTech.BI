namespace CoTech.Bi.Modules.Wer.Models.Responses
{
    public class ReportPendingsResponse
    {
        public long idWeek { set; get; }
        public long idUser { set; get; }
        public long idCompany { set; get; }
        public string User { set; get; }
        public bool create { set; get; }
    }
}