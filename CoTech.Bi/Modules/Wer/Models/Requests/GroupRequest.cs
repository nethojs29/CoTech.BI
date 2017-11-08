namespace CoTech.Bi.Modules.Wer.Models.Requests
{
    public class GroupRequest
    {
        public int Type { set; get; }
        
        public long UserId { set; get; }
        
        public long CompanyId { set; get; }
    }
}