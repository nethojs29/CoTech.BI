using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CoTech.Bi.Core.Companies.Models;

namespace CoTech.Bi.Core.Companies.Models
{
    public class CompanyResult {
      [Required]
      public long Id { get; set; }
      [Required]
      public string Name { get; set; }
      [Required]
      public string Activity { get; set; }
      [Required]
      public string Url { get; set; }
      public long? ParentId { get; set; }
      public DateTime? DeletedAt { get; set; }
      public List<long> Modules { get; set; }

      public CompanyResult(CompanyEntity entity){
        Id = entity.Id;
        Name = entity.Name;
        Activity = entity.Activity;
        Url = entity.Url;
        ParentId = entity.ParentId;
        DeletedAt = entity.DeletedAt;
        if(entity.Modules != null) {
          Modules = entity.Modules.Select(m => m.ModuleId).ToList();
        }
      }
      
    }
}