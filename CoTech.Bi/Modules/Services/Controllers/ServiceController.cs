using System.Threading.Tasks;
using CoTech.Bi.Modules.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Services.Controllers{
    [Route("api/companies/{idCompany}/services")]
    public class ServiceController:Controller{
        private readonly ServiceRepository serviceRepo;

        public ServiceController(ServiceRepository serviceRepo){
            this.serviceRepo = serviceRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> getAll(){
            return new OkObjectResult(await serviceRepo.getAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id){
            return new OkObjectResult(await serviceRepo.WithId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceReq req){
            var service = req.toEntity();
            await serviceRepo.Create(service);
            return Created($"/api/services/${service.Id}", service);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateServiceReq req){
            var result = await serviceRepo.Update(id, req);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var service = await serviceRepo.WithId(id);
            await serviceRepo.Delete(service);
            return new OkObjectResult(service);
        }
    }
}