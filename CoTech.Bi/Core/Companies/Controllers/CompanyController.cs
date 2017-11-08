using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Companies.Notifiers;
using CoTech.Bi.Core.Companies.Repositories;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Core.Permissions.Repositories;
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
        [RequiresAnyRole]
        [ProducesResponseType(typeof(CompanyResult), 200)]
        public async Task<IActionResult> GetById(long id) {
          return new OkObjectResult(await companyRepo.WithId(id));
        }

        [HttpGet("url={url}")]
        [ProducesResponseType(typeof(CompanyResult), 200)]
        public async Task<IActionResult> GetByUrl(string url){
          var userId = HttpContext.UserId();
          if(userId == null){
            return new UnauthorizedResult();
          }
          var company = await companyRepo.WithUrl(url);
          if(company == null){
            return new NotFoundResult();
          }
          var hasAnyRole = await permissionRepo.UserHasAnyRoleInCompany(userId.Value, company.Id, true, true);
          if(!hasAnyRole){
            return new UnauthorizedResult();
          }
          return new OkObjectResult(company);
        }

        [HttpGet("{id}/children")]
        [RequiresAbsoluteRole(Role.Super)]
        public async Task<IActionResult> GetCompanyChildren(long id) {
          var children = await companyRepo.ChildrenOf(id);
          return new OkObjectResult(children.Select(c => new CompanyResult(c)));
        }

        [HttpGet("mines")]
        [ProducesResponseType(typeof(CompanyResult), 200)]
        public async Task<IActionResult> GetMyCompanies() {
          var userId = HttpContext.UserId();
          if(userId == null){
            return new UnauthorizedResult();
          }
          var company = await companyRepo.GetUserCompanies(userId.Value);
          if(company == null){
            return new NotFoundResult();
          }
          return new OkObjectResult(company);
        }

        [HttpPost]
        [RequiresRoot]
        public async Task<IActionResult> Create([FromBody] CreateCompanyReq req){
          var cmd = new CreateCompanyCmd(req, HttpContext.UserId().Value);
          var company = await companyRepo.Create(cmd);
          return Created($"/api/companies/${company.Id}", new CompanyResult(company));
        }

        [HttpPut("{id}")]
        [RequiresAbsoluteRole(Role.Super)]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateCompanyReq req) {
          var updateCmd = new UpdateCompanyCmd(id, req, HttpContext.UserId().Value);
          var company = await companyRepo.Update(updateCmd);
          return Ok(new CompanyResult(company));
        }

        [HttpPost("{id}/modules/{moduleId}")]
        [RequiresRoot]
        public async Task<IActionResult> AddModule(long id, long moduleId) {
          var addModCmd = new AddModuleCmd(id, moduleId, HttpContext.UserId().Value);
          var added = await companyRepo.AddModule(addModCmd);
          if(added) {
            return NoContent();
          } else {
            return BadRequest();
          }
        }

        [HttpDelete("{id}/modules/{moduleId}")]
        [RequiresRoot]
        public async Task<IActionResult> RemoveModule(long id, long moduleId) {
          var removeModCmd = new RemoveModuleCmd(id, moduleId, HttpContext.UserId().Value);
          var removed = await companyRepo.RemoveModule(removeModCmd);
          if(removed) {
            return NoContent();
          } else {
            return BadRequest();
          }
        }

        [HttpDelete("{id}")]
        [RequiresRoot]
        public async Task<IActionResult> Delete(long id){
          var cmd = new DeleteCompanyCmd(id, HttpContext.UserId().Value);
          var company = await companyRepo.Delete(cmd);
          return Ok(company);
        }
    }
}