using System.Collections.Generic;
using System.Linq;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Modules.Wer.Models.Responses
{
    public class CompanyResponse
    {
        public CompanyEntity company { set; get; }
        public List<WerUserAndPermissions> Users { set; get; }
    }

    public class WerUserAndPermissions {
        public UserResponse User { get; set; }

        public WerUserAndPermissions(UserEntity entity)
        {
            User = new UserResponse(new UserEntity()
            {
                Id = entity.Id,
                Email = entity.Email,
                Name = entity.Name,
                Lastname = entity.Lastname,
            });
        }
    }
}