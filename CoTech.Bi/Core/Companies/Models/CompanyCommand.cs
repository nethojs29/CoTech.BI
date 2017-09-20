using CoTech.Bi.Core.EventSourcing.Models;

namespace CoTech.Bi.Core.Companies.Models
{
  public abstract class CompanyCommand : Command {}

  public class CreateCompanyCmd : CompanyCommand {
    public string Name { get; set; }
    public string Activity { get; set; }
    public string Url { get; set; }

    public CreateCompanyCmd(CreateCompanyReq req, long userId) {
      UserId = userId;
      Name = req.Name;
      Activity = req.Activity;
      Url = req.Url;
    }

  }

  public class UpdateCompanyCmd : CompanyCommand {
    public long Id { get; set; }
    public string Name { get; set; }
    public string Activity { get; set;}
    public string Url { get; set; }

    public UpdateCompanyCmd(long id, UpdateCompanyReq req, long userId) {
      UserId = userId;
      Id = id;
      Name = req.Name;
      Activity = req.Activity;
      Url = req.Url;
    }

  }

  public class AddModuleCmd : CompanyCommand {
    public long Id { get; set; }
    public long ModuleId { get; set; }
    public AddModuleCmd(long id, long moduleId, long userId) {
      UserId = userId;
      Id = id;
      ModuleId = moduleId;
    }
  }

  public class RemoveModuleCmd : CompanyCommand {
    public long Id { get; set; }
    public long ModuleId { get; set; }
    public RemoveModuleCmd(long id, long moduleId, long userId) {
      UserId = userId;
      Id = id;
      ModuleId = moduleId;
    }
  }

  public class DeleteCompanyCmd : CompanyCommand {
    public long Id { get; set; }
    public DeleteCompanyCmd(long id, long userId) {
      UserId = userId;
      Id = id;
    }

  }
}