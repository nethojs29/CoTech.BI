using System.Threading.Tasks;
using CoTech.Bi.Modules.Providers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CoTech.Bi.Modules.Providers.Controllers{
    [Route("api/companies/{idCompany}/providers")]
    public class ProviderController : Controller{
        private readonly ProviderRepository providerRepo;

        public ProviderController(ProviderRepository provierRepo){
            this.providerRepo = provierRepo;
        }

        [HttpGet]
        public async Task<IActionResult> getAll(){
            return new OkObjectResult(await providerRepo.getAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id){
            return new OkObjectResult(await providerRepo.WithId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProviderReq req){
            var provider = req.toEntity();
            await providerRepo.Create(provider);
            return Created($"/api/providers/${provider.Id}", provider);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateProviderReq req){
            var result = await providerRepo.Update(id, req);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var provider = await providerRepo.WithId(id);
            await providerRepo.Delete(provider);
            return new OkObjectResult(provider);
        }
    }
}