using System.Collections.Generic;
using System.Linq;
using CoTech.Bi.Core.Permissions.Model;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Core.Permissions.Models
{
    public class PermissionResponse
    {
        public long CompanyId { get; set; }
        public long RoleId { get; set; }

        public PermissionResponse(PermissionEntity entity) {
            CompanyId = entity.CompanyId;
            RoleId = entity.RoleId;
        }
    }

    class UserAndPermissions {
        public UserResponse User { get; set; }
        public List<PermissionResponse> Permissions { get; set; }

        public UserAndPermissions(UserEntity entity)
        {
            User = new UserResponse(entity);
            Permissions = entity.Permissions.Select(p => new PermissionResponse(p)).ToList();
        }
    }
}