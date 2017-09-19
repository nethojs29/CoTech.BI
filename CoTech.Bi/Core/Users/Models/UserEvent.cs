using CoTech.Bi.Core.EventSourcing.Models;
using Microsoft.AspNetCore.Identity;

namespace CoTech.Bi.Core.Users.Models
{
    public interface UserEvent {}

    public class UserCreatedEvt : UserEvent {
      public string Name { get; set; }
      public string Lastname { get; set; }
      public string Email { get; set; }

      public UserCreatedEvt(CreateUserCmd cmd) {
        Name = cmd.Name;
        Lastname = cmd.Lastname;
        Email = cmd.Email;
      }

      public static EventEntity MakeEventEntity(CreateUserCmd cmd) {
            return new EventEntity {
                UserId = cmd.UserId,
                Body = new UserCreatedEvt(cmd)
            };
        }
    }

    public class PasswordChangedEvt : UserEvent {
      public long UserId { get; set; }
      public string HashedPassword { get; set; }

      public PasswordChangedEvt(long userId, string hashedPassword) {
        UserId = userId;
        HashedPassword = hashedPassword;
      }

      public static EventEntity MakeEventEntity(CreateUserCmd cmd, long userId, string hashedPassword) {
        return new EventEntity {
          UserId = cmd.UserId,
          Body = new PasswordChangedEvt(userId, hashedPassword)
        };
      }
    }
}