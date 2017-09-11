using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Core.Users.Models
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

  public class UserNoPassReq : IUserRequest
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }

    public UserNoPassReq(long Id,string Name,string Lastname,string Email)
    {
      this.Id = Id;
      this.Name = Name;
      this.Lastname = Lastname;
      this.Email = Email;
    }
  }

  public struct ResetRequest : IUserRequest
  {
    public string email { set; get; }

    public ResetRequest(string email)
    {
      this.email = email;
    }
  }
}