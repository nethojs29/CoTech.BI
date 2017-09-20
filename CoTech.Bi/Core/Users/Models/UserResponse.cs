using System;
using System.Collections.Generic;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Core.Users.Models;

namespace CoTech.Bi.Core.Users.Models
{
    public class UserResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public UserResponse(UserEntity entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Lastname = entity.Lastname;
            this.Email = entity.Email;
        }
    }

    public class AuthResponse {
      public string Token { get; set; }
      public DateTime Expiration { get; set;}
      public UserResponse User { get; set;}
      public bool IAmRoot { get; set; }
      public List<PermissionResponse> Permissions { get; set; }
      public List<CompanyResult> Companies { get; set; }
    }
}