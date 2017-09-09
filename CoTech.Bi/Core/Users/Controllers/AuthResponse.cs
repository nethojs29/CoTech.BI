using System;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Core.Users.Controllers
{
    public class AuthResponse {
      public string Token { get; set; }
      public DateTime Expiration { get; set;}
      public UserEntity User { get; set;}
    }
}