using CoTech.Bi.Core.Permissions.Model;

namespace CoTech.Bi.Core.Permissions.Controllers
{
    public class PermissionResponse
    {
        public long CompanyId { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }

        public PermissionResponse(PermissionEntity entity){
          CompanyId = entity.CompanyId;
          UserId = entity.UserId;
          RoleId = entity.RoleId;
        }
    }
}