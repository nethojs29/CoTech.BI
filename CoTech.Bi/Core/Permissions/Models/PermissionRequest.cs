using System.ComponentModel.DataAnnotations;

namespace CoTech.Bi.Core.Permissions.Models
{
    public class CreatePermissionReq {
      [Required]
      public long? RoleId { get; set; }
    }
}