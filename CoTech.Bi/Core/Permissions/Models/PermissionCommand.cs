using CoTech.Bi.Core.EventSourcing.Models;

namespace CoTech.Bi.Core.Permissions.Models
{
  public abstract class PermissionCommand : Command {}

  public class GivePermissionCmd : PermissionCommand
  {
    public long CompanyId { get; set; }
    /// <summary>
    ///   Usuario al que se le van a dar permisos
    /// </summary>
    /// <returns></returns>
    public long User2Id { get; set; }
    public long RoleId { get; set; }
    public GivePermissionCmd(long companyId, long userId, CreatePermissionReq req, long user2Id) {
      UserId = userId;
      CompanyId = companyId;
      RoleId = req.RoleId.Value;
      User2Id = user2Id;
    }

  }

  public class RemoveRoleCmd : PermissionCommand
  {
    public long CompanyId { get; set; }
    public long User2Id { get; set; }
    public long RoleId { get; set; }
    public RemoveRoleCmd(long companyId, long userId, long roleId, long user2Id) {
      CompanyId = companyId;
      UserId = userId;
      RoleId = roleId;
      User2Id = user2Id;
    }
   
  }

  public class RevokePermissionsCmd : PermissionCommand {
    public long CompanyId { get; set; }
    public long User2Id { get; set; }
    public RevokePermissionsCmd(long companyId, long userId, long user2Id) {
      CompanyId = companyId;
      UserId = userId;
      User2Id = user2Id;
    }

  }
}