namespace CoTech.Bi.Modules.Wer.Models.Events
{
    public class ReportEvent
    {
        
    }

    public class ReportUpdatedEvt : ReportEvent
    {
        public long Id { set; get; }
        public string Operative { set; get; }
        public string Financial { set; get; }
        public string Observation{ set; get; }
    }
}