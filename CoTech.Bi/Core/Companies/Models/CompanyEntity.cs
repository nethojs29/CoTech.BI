using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoTech.Bi.Core.EventSourcing.Models;

namespace CoTech.Bi.Core.Companies.Models
{
    public class CompanyEntity {
        public long Id { get; set; }
        public long? CreatorEventId { get; set; }
        public EventEntity CreatorEvent { get; set; }
        [Required]
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
        public List<CompanyToModule> Modules { get; set; }
        public List<DepartmentEntity> Departments{ set; get; }
    }

    public class DepartmentEntity{
        public long Id { get; set; }
        public long? CreatorEventId { get; set; }
        public EventEntity CreatorEvent { get; set; }
        [Required]
        public string Name{ set; get; }
        [Required]
        public long CompanyId{ set; get; }
        
        public CompanyEntity Company{ set; get; }
        
        public DateTime CreatedAt{ set; get; }
        public DateTime? DeletedAt{ set; get; }
        
    }
}