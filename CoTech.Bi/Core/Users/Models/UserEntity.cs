using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CoTech.Bi.Core.Users.Models
{
    public class UserEntity
    {
        public long Id { get; set; }
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
    }
}