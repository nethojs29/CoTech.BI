using System;
using System.Collections.Generic;
using CoTech.Bi.Core.EventSourcing.Models;

namespace CoTech.Bi.Core.Companies.Models
{
    public class CompanyEvent {}
    public class CompanyCreatedEvt : CompanyEvent
    {
        public string Name {get; set;}
        public string Activity { get; set; }
        public string Url { get; set; }
        public long? ParentId { get; set; }
        public string Color { get; set; }
        public List<long> Modules { get; set; }
        public CompanyCreatedEvt(CreateCompanyCmd cmd) { 
            Name = cmd.Name;
            Activity = cmd.Activity;
            Url = cmd.Url;
            ParentId = cmd.ParentId;
            Color = cmd.Color;
            Modules = cmd.Modules;
        }

        public static EventEntity MakeEventEntity(CreateCompanyCmd cmd) {
            return new EventEntity {
                UserId = cmd.UserId,
                Body = new CompanyCreatedEvt(cmd)
            };
        }
    }

    public class CompanyUpdatedEvt : CompanyEvent {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Activity { get; set;}
        public string Url { get; set; }

         public CompanyUpdatedEvt(UpdateCompanyCmd cmd) {
            Id = cmd.Id;
            Name = cmd.Name;
            Activity = cmd.Activity;
            Url = cmd.Url;
        }

        public static EventEntity MakeEventEntity(UpdateCompanyCmd cmd) {
            return new EventEntity {
                UserId = cmd.UserId,
                Body = new CompanyUpdatedEvt(cmd)
            };
        }
    }

    public class ModuleAddedEvt : CompanyEvent {
        public long Id {get; set; }
        public long ModuleId { get; set; }
        public ModuleAddedEvt(AddModuleCmd cmd) {
            Id = cmd.Id;
            ModuleId = cmd.ModuleId;
        }
        public static EventEntity MakeEventEntity(AddModuleCmd cmd) {
            return new EventEntity {
                UserId = cmd.Id,
                Body = new ModuleAddedEvt(cmd)
            };
        }
    }

    public class ModuleRemovedEvt : CompanyEvent {
        public long Id { get; set; }
        public long ModuleId { get; set; }
        public ModuleRemovedEvt(RemoveModuleCmd cmd) {
            Id = cmd.Id;
            ModuleId = cmd.ModuleId;
        }
        public static EventEntity MakeEventEntity(RemoveModuleCmd cmd) {
            return new EventEntity {
                UserId = cmd.Id,
                Body = new ModuleRemovedEvt(cmd)
            };
        }
    }

    public class CompanyDeletedEvt : CompanyEvent {
        public long Id { get; set; }
        public CompanyDeletedEvt(DeleteCompanyCmd cmd)
        {
            Id = cmd.Id;
        }
        public static EventEntity MakeEventEntity(DeleteCompanyCmd cmd) {
            return new EventEntity {
                UserId = cmd.UserId,
                Body = new CompanyDeletedEvt(cmd)
            };
        }
    }
}