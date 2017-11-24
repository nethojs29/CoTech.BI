using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Companies.Repositories;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Core.Permissions.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Wer.Controllers
{

    [Route("/api/companies/{companyId}/res/enterprises")]
    public class EnterpriseController : Controller
    {
        private readonly CompanyRepository companyRepo;
        private readonly PermissionRepository permissionRepo;

        public EnterpriseController(CompanyRepository companyRepo, PermissionRepository permissionRepo)
        {
            this.companyRepo = companyRepo;
            this.permissionRepo = permissionRepo;
        }

        [HttpPost("child")]
        [RequiresAuth]
        // [RequiresAbsoluteRole(Role.Super, Role.Admin)]
        public async Task<IActionResult> CreateChildren(long companyId, [FromBody] CreateCompanyReq req){
            req.ParentId = companyId;
            var userId = HttpContext.UserId().Value;
            var cmd = new CreateCompanyCmd(req, userId);
            var company = await companyRepo.Create(cmd);
            var cmd2 = new GivePermissionCmd(
                company.Id, 
                userId,
                new CreatePermissionReq { RoleId = 601 },
                userId
            );
            var permission = await permissionRepo.Create(cmd2);
            return Created($"/api/companies/${company.Id}",  new CompanyResult(company));
        }
    }
}