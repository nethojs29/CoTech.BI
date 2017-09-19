using CoTech.Bi.Core.EventSourcing.Models;

namespace CoTech.Bi.Core.Permissions.Models
{
    public interface PermissionEvent { }

    public class PermissionGivenEvt : PermissionEvent
    {
        public long CompanyId { get; set; }
        public long UserId {get; set; }
        public long RoleId { get; set; }
        public PermissionGivenEvt(GivePermissionCmd cmd) {
          CompanyId = cmd.CompanyId;
          UserId = cmd.User2Id;
          RoleId = cmd.RoleId;
        }
        public static EventEntity MakeEventEntity(GivePermissionCmd cmd) {
            return new EventEntity {
                UserId = cmd.UserId,
                Body = new PermissionGivenEvt(cmd)
            };
        }
    }

    public class RoleRemovedEvt : PermissionEvent {
        public long CompanyId { get; set; }
        public long UserId {get; set; }
        public long RoleId { get; set; }
        public RoleRemovedEvt(RemoveRoleCmd cmd) {
          CompanyId = cmd.CompanyId;
          UserId = cmd.User2Id;
          RoleId = cmd.RoleId;
        }
        public static EventEntity MakeEventEntity(RemoveRoleCmd cmd) {
            return new EventEntity {
                UserId = cmd.UserId,
                Body = new RoleRemovedEvt(cmd)
            };
        }
    }

    public class PermissionsRevokedEvt : PermissionEvent {
      public long CompanyId { get; set; }
      public long UserId { get; set; }
      public PermissionsRevokedEvt(RevokePermissionsCmd cmd) {
        CompanyId = cmd.CompanyId;
        UserId = cmd.User2Id;
      }
      public static EventEntity MakeEventEntity(RevokePermissionsCmd cmd) {
            return new EventEntity {
                UserId = cmd.UserId,
                Body = new PermissionsRevokedEvt(cmd)
            };
        }
    }
}