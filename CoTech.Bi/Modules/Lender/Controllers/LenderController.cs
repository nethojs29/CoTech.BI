using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Lender.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Lender.Controllers{
    [Route("api/companies/{idCompany}/lenders")]
    public class LenderController : Controller{
        private readonly LenderRepository lenderRepo;

        public LenderController(LenderRepository lenderRepo){
            this.lenderRepo = lenderRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> getAll(){
            return new OkObjectResult(await lenderRepo.getAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id){
            return new OkObjectResult(await lenderRepo.WithId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLenderReq req){
            var lender = req.toEntity(HttpContext.UserId().Value);
            await lenderRepo.Create(lender);
            return Created($"/api/lenders/${lender.Id}", lender);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateLenderReq req){
            var result = await lenderRepo.Update(id, req);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var lender = await lenderRepo.WithId(id);
            await lenderRepo.Delete(lender);
            return new OkObjectResult(lender);
        }
    }
}