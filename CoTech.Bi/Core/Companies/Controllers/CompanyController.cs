using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Permissions.Model;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Core.Companies.Controllers
{
    /// <summary>
    /// Controlador para crear, ver, modificar y eliminar (soft delete) empresas
    /// </summary>
    [Route("/api/companies")]
    public class CompanyController : Controller
    {
        private readonly CompanyRepository companyRepo;
        private readonly PermissionRepository permissionRepo;
        private readonly CompanyNotifier companyNotifier;

    /// <summary>
    /// Constructor injectable
    /// </summary>
    /// <param name="companyRepo"></param>
    /// <param name="permissionRepo"></param>
    /// <param name="companyNotifier"></param>
    public CompanyController(CompanyRepository companyRepo, 
                                 PermissionRepository permissionRepo, 
                                 CompanyNotifier companyNotifier) {
          this.companyRepo = companyRepo;
          this.permissionRepo = permissionRepo;
          this.companyNotifier = companyNotifier;
    }

        /// <summary>
        /// Obtiene todas las empresas
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/companies
        ///     [
        ///       {
        ///         "id": 2,
        ///         "name": "CoTecnologias",
        ///         "activity: "Desarrollo de Software",
        ///         "url": "cot",
        ///         "parentId": 1
        ///       }
        ///     ]
        /// 
        /// </remarks>
        /// <returns>Todas las empresas</returns>
        /// <response code="200">Regresa todas las empresas</response>
        /// <response code="401">Si el usuario no es Root (-1)</response>
        [HttpGet]
        [RequiresRoot]
        [ProducesResponseType(typeof(List<CompanyResult>), 200)]
        public async Task<IActionResult> GetAll(){
          var companies = await companyRepo.GetAll();
          return new OkObjectResult(companies.Select(c => new CompanyResult(c)));
        }

        /// <summary>
        /// Obtiene una empresa dado su Id
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// 
        ///     GET /api/companies/2
        ///     {
        ///       "id": 2,
        ///       "name": "CoTecnologias",
        ///       "activity: "Desarrollo de Software",
        ///       "url": "cot",
        ///       "parentId": 1
        ///     }
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>La empresa con el Id dado</returns>
        /// <response code="200">Empresa con Id</response>
        /// <response code="401">
        /// Si el usuario no es root, no tiene permisos super en ancestros, 
        /// y no tiene ningun permiso en la empresa
        /// </response>
        [HttpGet("{id}")]
        [RequiresPermission]
        [ProducesResponseType(typeof(CompanyResult), 200)]
        public async Task<IActionResult> GetById(long id) {
          return new OkObjectResult(await companyRepo.WithId(id));
        }

        [HttpGet("url={url}")]
        [ProducesResponseType(typeof(CompanyResult), 200)]
        public async Task<IActionResult> GetByUrl(string url){
          var userId = HttpContext.UserId();
          if(userId == -1){
            return new UnauthorizedResult();
          }
          var company = await companyRepo.WithUrl(url);
          if(company == null){
            return new NotFoundResult();
          }
          var hasAnyRole = await permissionRepo.UserHasAnyRoleInCompany(userId, company.Id, true, true);
          if(!hasAnyRole){
            return new UnauthorizedResult();
          }
          return new OkObjectResult(company);
        }

        [HttpGet("{id}/children")]
        [RequiresImportantRole(Role.Super)]
        public async Task<IActionResult> GetCompanyChildren(long id) {
          var children = await companyRepo.ChildrenOf(id);
          return new OkObjectResult(children);
        }

        [HttpGet("mines")]
        [ProducesResponseType(typeof(CompanyResult), 200)]
        public async Task<IActionResult> GetMyCompanies() {
          var userId = HttpContext.UserId();
          if(userId == -1){
            return new UnauthorizedResult();
          }
          var company = await companyRepo.GetUserCompanies(userId);
          if(company == null){
            return new NotFoundResult();
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
          await companyNotifier.Created(company, HttpContext.UserId());
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

        [HttpDelete("{id}")]
        [RequiresRoot]
        public async Task<IActionResult> Delete(long id){
          var company = await companyRepo.WithId(id);
          await companyRepo.Delete(company);
          return new OkObjectResult(company);
        }
    }
}