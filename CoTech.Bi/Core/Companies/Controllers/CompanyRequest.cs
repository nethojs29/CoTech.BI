using CoTech.Bi.Core.Companies.Models;

namespace CoTech.Bi.Core.Companies.Controllers
{
    public interface CompanyRequest {}

    public class CreateCompanyReq : CompanyRequest {
        public string Name { get; set; }
        public string Activity { get; set; }
        public string Url { get; set; }

        public CompanyEntity ToEntity() {
            return new CompanyEntity {
                Name = Name,
                Activity = Activity,
                Url = Url
            };
        }
    }
}