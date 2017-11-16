namespace CoTech.Bi.Modules.Wer.Models.Requests
{
    public class MessageRequest
    {
        public string Message { set; get; }
        public string Tags { set; get; }
        public long WeekId { set; get; }
    }
}