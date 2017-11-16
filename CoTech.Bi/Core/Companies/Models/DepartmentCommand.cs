using CoTech.Bi.Core.EventSourcing.Models;

namespace CoTech.Bi.Core.Companies.Models{
    public abstract class DepartmentCommand : Command {}

    public class CreateDepartmentCmd : DepartmentCommand {
        public string Name { get; set; }

        public CreateDepartmentCmd(CreateDepartmentReq req, long userId) {
            UserId = userId;
            Name = req.Name;
        }

    }

    public class UpdateDepartmentCmd : DepartmentCommand {
        public long Id { get; set; }
        public string Name { get; set; }

        public UpdateDepartmentCmd(long id, UpdateDepartmentReq req, long userId) {
            UserId = userId;
            Id = id;
            Name = req.Name;
        }

    }

    public class DeleteDepartmentCmd : DepartmentCommand {
        public long Id { get; set; }
        public DeleteDepartmentCmd(long id, long userId) {
            UserId = userId;
            Id = id;
        }

    }
}