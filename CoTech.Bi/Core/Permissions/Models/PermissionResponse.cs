using System;
using CoTech.Bi.Core.Permissions.Model;

namespace CoTech.Bi.Core.Permissions.Models
{
    public class PermissionResponse
    {
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
        public long RoleId { get; set; }

        public PermissionResponse(PermissionEntity entity){
          CompanyId = entity.CompanyId;
          UserId = entity.UserId;
          RoleId = entity.RoleId;
        }
    }
}