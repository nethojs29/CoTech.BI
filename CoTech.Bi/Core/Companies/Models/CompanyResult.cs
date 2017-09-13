using System;
using System.ComponentModel.DataAnnotations;
using CoTech.Bi.Core.Companies.Models;

namespace CoTech.Bi.Core.Companies.Models
{
    public class CompanyResult {
      [Required]
      public Guid Id { get; set; }
      [Required]
      public string Name { get; set; }
      [Required]
      public string Activity { get; set; }
      [Required]
      public string Url { get; set; }
      public Guid? ParentId { get; set; }
      public DateTime? DeletedAt { get; set; }

      public CompanyResult(CompanyEntity entity){
        Id = entity.Id;
        Name = entity.Name;
        Activity = entity.Activity;
        Url = entity.Url;
        ParentId = entity.ParentId;
        DeletedAt = entity.DeletedAt;
      }
      
    }
}