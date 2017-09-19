using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.EventSourcing.Models;
using CoTech.Bi.Core.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace CoTech.Bi.Core.Permissions.Models
{
    public class PermissionEntity
    {
        public long Id { get; set; }
        public long? CreatorEventId { get; set; }
        public EventEntity CreatorEvent { get; set; }
        public long CompanyId { get; set; }
        public CompanyEntity Company { get; set; }
        public long UserId { get; set; }
        public UserEntity User { get; set; }
        public long RoleId { get; set; }
    }

    public class RootEntity {
        public long Id {get; set; }
        public long UserId { get; set; }
        public UserEntity User { get; set; }
    }
}