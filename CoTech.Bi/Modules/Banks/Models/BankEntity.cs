﻿using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace CoTech.Bi.Modules.Banks.Models{
    public class BankEntity{
        public long Id{ set; get;}
        [Required]
        public string Name{ set; get; }
        [Required]
        public string RFC{ set; get; }
        [Required]
        public string Bank{ set; get; }
        [Required]
        public string Account{ set; get; }
        [Required]
        public int Status{ set; get; }
        [Required]
        public long CompanyId{set; get;}
        public CompanyEntity Company{set; get;}
        public long CreatorId{ set; get; }
        public UserEntity Creator{ set; get; }
        [Required]
        public DateTime CreatedAt{set; get;}
        public DateTime? DeletedAt{set; get;}
    }
}