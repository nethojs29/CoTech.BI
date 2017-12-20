using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Modules.Wer.Models.Entities
{
    public class FileCompanyEntity
    {
        public long Id { set; get; }
        public long CompanyId { set; get; }
        public CompanyEntity Company { set; get; }
        public long WeekId { set; get; }
        public WeekEntity Week { set; get; }
        public long UserId { set; get; }
        public UserEntity User { set; get; }
        
        public string Name { set; get; }
        public string Mime { set; get; }
        public string Uri { set; get; }
        public string Extension { set; get; }
    }
}