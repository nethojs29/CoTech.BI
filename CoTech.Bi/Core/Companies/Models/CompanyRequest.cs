using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CoTech.Bi.Core.Companies.Models;

namespace CoTech.Bi.Core.Companies.Models
{
    public interface CompanyRequest {}

    public class CreateCompanyReq : CompanyRequest {
        public string Name { get; set; }
        public string Activity { get; set; }
        public string Url { get; set; }
        public long? ParentId { get; set; }
        public string Color { get; set; }
        public List<long> Modules { get; set; }

        public CompanyEntity ToEntity() {
            return new CompanyEntity {
                ParentId = ParentId,
                Name = Name,
                Activity = Activity,
                Url = Url,
                Color = Color,
                Modules = Modules != null
                    ? Modules.Select(mid => new CompanyToModule { ModuleId = mid }).ToList() 
                    : new List<CompanyToModule>()
            };
        }
    }

    public class UpdateCompanyReq : CompanyRequest {
        public string Name { get; set; }
        public string Activity { get; set; }
        public string Url { get; set; }

    }
}