using CoTech.Bi.Core.EventSourcing.Models;

namespace CoTech.Bi.Core.Companies.Models{
    public class DepartmentEvent {}
    public class DepartmentCreatedEvt : DepartmentEvent
    {
        public string Name {get; set;}
        public string Activity { get; set; }
        public string Url { get; set; }

        public DepartmentCreatedEvt(CreateDepartmentCmd cmd) { 
            Name = cmd.Name;
        }

        public static EventEntity MakeEventEntity(CreateDepartmentCmd cmd) {
            return new EventEntity {
                UserId = cmd.UserId,
                Body = new DepartmentCreatedEvt(cmd)
            };
        }
    }

    public class DepartmentUpdatedEvt : DepartmentEvent {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Activity { get; set;}
        public string Url { get; set; }

         public DepartmentUpdatedEvt(UpdateDepartmentCmd cmd) {
            Id = cmd.Id;
            Name = cmd.Name;
        }

        public static EventEntity MakeEventEntity(UpdateDepartmentCmd cmd) {
            return new EventEntity {
                UserId = cmd.UserId,
                Body = new DepartmentUpdatedEvt(cmd)
            };
        }
    }

    public class DepartmentDeletedEvt : DepartmentEvent {
        public long Id { get; set; }
        public DepartmentDeletedEvt(DeleteDepartmentCmd cmd)
        {
            Id = cmd.Id;
        }
        public static EventEntity MakeEventEntity(DeleteDepartmentCmd cmd) {
            return new EventEntity {
                UserId = cmd.UserId,
                Body = new DepartmentDeletedEvt(cmd)
            };
        }
    }
}