using System;
using System.ComponentModel.DataAnnotations;

namespace CoTech.Bi.Core.Companies.Models
{
    public class CompanyEntity {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Activity { get; set; }
        [Required]
        public string Url { get; set; }
        public Guid? ParentId { get; set; }
        public CompanyEntity Parent { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string PhotoUrl { get; set; }
        public string Color { get; set; }
    }
}