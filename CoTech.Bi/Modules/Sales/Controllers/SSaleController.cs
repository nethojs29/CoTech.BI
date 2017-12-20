using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Sales.Models;
using CoTech.Bi.Modules.Sales.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Sales.Controllers{
    [Route("api/companies/{idCompany}/services/sales")]
    public class SSaleController:Controller{
        private readonly SSaleRepository ssRepo;

        public SSaleController(SSaleRepository ssRepo){
            this.ssRepo = ssRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> getAll(long idCompany){
            return new OkObjectResult(await ssRepo.getAll(idCompany));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id){
            return new OkObjectResult(await ssRepo.WithId(id));
        }

        [HttpGet("client/{idClient}")]
        public async Task<IActionResult> getAllByClient(long idClient){
            return new OkObjectResult(await ssRepo.getAllByClient(idClient));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSSaleRequest req){
            var sale = req.toEntity(HttpContext.UserId().Value);
            await ssRepo.Create(sale);
            return Created($"/api/sales/${sale.Id}", sale);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateSSaleRequest req){
            var result = await ssRepo.Update(id, req);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var sale = await ssRepo.WithId(id);
            await ssRepo.Delete(sale);
            return new OkObjectResult(sale);
        }
    }
}