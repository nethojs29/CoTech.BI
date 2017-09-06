namespace CoTech.Bi.Core.Companies.Models
{
    public class CompanyEntity {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Activity { get; set; }
        public string Url { get; set; }
        public int? Parent { get; set; }
    }
}