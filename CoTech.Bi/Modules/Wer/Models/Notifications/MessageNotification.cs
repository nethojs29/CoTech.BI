using CoTech.Bi.Modules.Wer.Models.Responses;

namespace CoTech.Bi.Modules.Wer.Models.Notifications
{
    public class MessageNotification
    {
        public long UserId { set; get; }
        public long WeekId { set; get; }
        public long CompanyId { set; get; }
        public long Type { set; get; }
        public PartyResponse User { set; get; }
    }
}