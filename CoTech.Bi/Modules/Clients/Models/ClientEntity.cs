using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Companies.Models;
using Microsoft.AspNetCore.Identity;

namespace CoTech.Bi.Modules.Clients.Models {
    public enum states{
        baja=-1,
        inactivo=0,
        activo=1
    }
    public class ClientEntity{
        public long Id {set; get;}
        [Required]
        public string Name{set; get;}
        [Required]
        public string Tradename{set; get;}
        [Required]
        public string RFC{set; get;}
        [Required]
        public string Address{set; get;}
        [Required]
        public string City{set; get;}
        [Required]
        public string State{set; get;}
        [Required]
        public string Email{set; get;}
        [Required]
        public string Phone{set; get;}
        [Required]
        public long CompanyId{set; get;}
        [Required]
        public CompanyEntity Company{set; get;}
        public int Status{set; get;}
        [Required]
        public DateTime CreatedAt{set; get;}
        public DateTime? DeletedAt{set; get;}

    }
}