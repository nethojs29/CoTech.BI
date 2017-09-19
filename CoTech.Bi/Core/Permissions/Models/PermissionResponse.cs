using System;
using CoTech.Bi.Core.Permissions.Models;

namespace CoTech.Bi.Core.Permissions.Models
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