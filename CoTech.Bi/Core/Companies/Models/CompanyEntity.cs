using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoTech.Bi.Core.EventSourcing.Models;

namespace CoTech.Bi.Core.Companies.Models
{
    public class CompanyEntity {
        public long Id { get; set; }
        [Required]
        public long? CreatorEventId { get; set; }
        public EventEntity CreatorEvent { get; set; }
        public string Name { get; set; }
        [Required]
        public string Activity { get; set; }
        [Required]
        public string Url { get; set; }
        public long? ParentId { get; set; }
        public CompanyEntity Parent { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string PhotoUrl { get; set; }
        public string Color { get; set; }
        
        public List<CompanyEntity> Children { set; get; }
    }
}