using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Sales.Models;
using CoTech.Bi.Modules.Sales.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Sales.Controllers{
    [Route("api/companies/{idCompany}/services/daily-sales")]
    public class ServiceSaleController:Controller{
        private readonly ServiceSaleRepository serviceSaleRepo;

        public ServiceSaleController(ServiceSaleRepository provierRepo){
            this.serviceSaleRepo = provierRepo;
        }

        [HttpGet]
        public async Task<IActionResult> getAll(long idCompany){
            return new OkObjectResult(await serviceSaleRepo.getAll(idCompany));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id){
            return new OkObjectResult(await serviceSaleRepo.WithId(id));
        }

        [HttpGet("client/{idClient}")]
        public async Task<IActionResult> getAllByClient(long idClient){
            return new OkObjectResult(await serviceSaleRepo.getAllByClient(idClient));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceSaleReq req){
            var sale = req.toEntity(HttpContext.UserId().Value);
            await serviceSaleRepo.Create(sale);
            return Created($"/api/sales/${sale.Id}", sale);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateServiceSaleReq req){
            var result = await serviceSaleRepo.Update(id, req);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var sale = await serviceSaleRepo.WithId(id);
            await serviceSaleRepo.Delete(sale);
            return new OkObjectResult(sale);
        }
    }
}