namespace CoTech.Bi.Core.Companies.Models
{
    public class CompanyToModule {
      public long Id { get; set; }
      public long CompanyId { get; set; }
      public CompanyEntity Company { get; set; }
      public long ModuleId { get; set; }
    }
}