namespace CoTech.Bi.Modules.Wer.Models.Notifications
{
    public class ReportUpdated
    {
        public long Id { set; get; }
        public long WeekId { set; get; }
        public long UserId { set; get; }
        public long CompanyId { set; get; }
    }
}