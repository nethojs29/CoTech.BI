using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoTech.Bi.Core.EventSourcing.Models;
using CoTech.Bi.Core.Permissions.Models;
using Microsoft.AspNetCore.Identity;

namespace CoTech.Bi.Core.Users.Models
{
    public class UserEntity
    {
        public long Id { get; set; }
        [ForeignKey("CreatorEvent")]
        public long? CreatorEventId { get; set; }
        public EventEntity CreatorEvent { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Lastname { get; set; }
        public string Password { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime? DeletedAt { get; set; }
        public List<PermissionEntity> Permissions { get; set; }
        public RootEntity Root { get; set; }
    }
}