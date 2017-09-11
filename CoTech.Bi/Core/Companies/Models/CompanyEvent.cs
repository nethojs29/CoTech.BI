namespace CoTech.Bi.Core.Companies.Models
{
    public interface CompanyEvent {}
    public class CompanyCreatedEvt : CompanyEvent
    {
        public string Name {get; set;}
        public string Activity { get; set; }
        public string Url { get; set; }
    }
}