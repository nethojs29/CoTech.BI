using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Permissions.Model;
using Microsoft.AspNetCore.Identity;

namespace CoTech.Bi.Core.Users.Models
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Password { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime? DeletedAt { get; set; }
        public List<PermissionEntity> Permissions { get; set; }
        public RootEntity Root { get; set; }
    }
}