using System.Threading.Tasks;
using CoTech.Bi.Modules.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Services.Controllers{
    [Route("api/services_prices")]
    public class Service_Price_ClientController:Controller{
        private readonly Service_Price_ClientRepository spcRepo;

        public Service_Price_ClientController(Service_Price_ClientRepository spcRepo){
            this.spcRepo = spcRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> getAll(){
            return new OkObjectResult(await spcRepo.getAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id){
            return new OkObjectResult(await spcRepo.WithId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServicePriceClientReq req){
            var service = req.toEntity();
            await spcRepo.Create(service);
            return Created($"/api/services/${service.Id}", service);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateServicePriceClientReq req){
            var result = await spcRepo.Update(id, req);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var service = await spcRepo.WithId(id);
            await spcRepo.Delete(service);
            return new OkObjectResult(service);
        }
    }
}