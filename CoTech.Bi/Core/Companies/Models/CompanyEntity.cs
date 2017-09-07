using System;
using System.ComponentModel.DataAnnotations;

namespace CoTech.Bi.Core.Companies.Models
{
    public class CompanyEntity {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Activity { get; set; }
        [Required]
        public string Url { get; set; }
        public long? ParentId { get; set; }
        public CompanyEntity Parent { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}