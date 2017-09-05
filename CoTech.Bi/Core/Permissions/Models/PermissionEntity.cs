using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CoTech.Bi.Core.Permissions.Model
{
    public class PermissionEntity
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }
}