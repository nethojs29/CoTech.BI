using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Core.Companies.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Core.Companies.Controllers
{
    [Route("/api/companies")]
    public class CompanyController : Controller
    {
        private readonly CompanyRepository companyRepo;

        public CompanyController(CompanyRepository companyRepo) {
            this.companyRepo = companyRepo;
        }

        [HttpGet]
        [RequiresRoot]
        public async Task<IActionResult> GetAll(){
          return new OkObjectResult(await companyRepo.GetAll());
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
    }
}