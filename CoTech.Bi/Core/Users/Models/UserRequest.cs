using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Core.Users.Models
{
  public interface IUserRequest {}

  public class CreateUserReq : IUserRequest {
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
  }
  
  public class CreateUserPasswordReq : IUserRequest {
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
  }

  public class UpdateUserReq : IUserRequest {
    public string Name { get; set; }
    public string Lastname { get; set; }
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

  public class ResetRequest : IUserRequest
  {
    public string email { set; get; }
  }

  public class ChangePasswordReq : IUserRequest {
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
  }
}