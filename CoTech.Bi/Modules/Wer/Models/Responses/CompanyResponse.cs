using System.Collections.Generic;
using System.Linq;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Permissions.Model;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Modules.Wer.Models.Responses
{
    public class CompanyResponse
    {
        public CompanyEntity company { set; get; }
        public List<UserAndPermissions> Users { set; get; }
    }

    public class UserAndPermissions {
        public UserResponse User { get; set; }

        public UserAndPermissions(UserEntity entity,List<PermissionEntity> permissionUSer)
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