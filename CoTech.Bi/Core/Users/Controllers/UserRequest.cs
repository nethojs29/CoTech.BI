using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Core.Users.Controllers
{
  public interface IUserRequest {}

  public class CreateUserReq : IUserRequest {
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }

    public UserEntity toEntity(){
      return new UserEntity {
        Name = Name, Lastname = Lastname, Email = Email
      };
    }
  }

  public class LogInReq : IUserRequest {
    public string Email { get; set; }
    public string Password { get; set; }
  }
}