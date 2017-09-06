using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Permissions.Model;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Core.Companies.Controllers
{
    [Route("/api/companies")]
    public class CompanyController : Controller
    {
        private readonly CompanyRepository companyRepo;
        private readonly PermissionRepository permissionRepo;

        public CompanyController(CompanyRepository companyRepo, PermissionRepository permissionRepo) {
          this.companyRepo = companyRepo;
          this.permissionRepo = permissionRepo;
        }

        /// <summary>
        /// Obtiene todas las empresas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [RequiresRoot]
        public async Task<IActionResult> GetAll(){
          return new OkObjectResult(await companyRepo.GetAll());
        }

        [HttpGet("{id}")]
        [RequiresPermission]
        public async Task<IActionResult> GetById(long id) {
          return new OkObjectResult(await companyRepo.WithId(id));
        }

        [HttpGet("url={url}")]
        public async Task<IActionResult> GetByUrl(string url){
          var userId = HttpContext.UserId();
          if(userId == -1){
            return new UnauthorizedResult();
          }
          var company = await companyRepo.WithUrl(url);
          var hasAnyRole = await permissionRepo.UserHasAnyRoleInCompany(userId, company.Id, true, true);
          if(!hasAnyRole){
            return new UnauthorizedResult();
          }
          return new OkObjectResult(company);
        }

        [HttpPost]
        [RequiresRoot]
        public async Task<IActionResult> Create([FromBody] CreateCompanyReq req){
          var companyWithUrl = await companyRepo.WithUrl(req.Url);
          if(companyWithUrl != null){
            return new BadRequestResult();
          }
          var company = req.ToEntity();
          await companyRepo.Create(company);
          return new CreatedResult($"/api/companies/${company.Id}", company);
        }

        [HttpPut("{id}")]
        [RequiresImportantRole(Role.Super)]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateCompanyReq req) {
          var company = await companyRepo.WithId(id);
          if(req.Name != null) company.Name = req.Name;
          if(req.Activity != null) company.Activity = req.Activity;
          if(req.Url != null){
            var companyWithUrl = await companyRepo.WithUrl(req.Url);
            if(companyWithUrl != null && companyWithUrl.Id == company.Id){
              return new BadRequestObjectResult("url ya está en uso");
            }
            company.Url = req.Url;
          }
          await companyRepo.Update(company);
          return new OkObjectResult(company);
        }

        [HttpDelete("{delete}")]
        [RequiresRoot]
        public async Task<IActionResult> Delete(long id){
          var company = await companyRepo.WithId(id);
          await companyRepo.Delete(company);
          return new OkObjectResult(company);
        }
    }
}