using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Invoices.Repositories;
using CoTech.Bi.Modules.Invoices.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Invoices.Controllers{
    [Route("api/companies/{idCompany}/invoices")]
    public class InvoiceController : Controller{
        private readonly InvoiceRepository repo;

        public InvoiceController(InvoiceRepository repo){
            this.repo = repo;
        }
        
        [HttpGet]
        public async Task<IActionResult> getAll(long idCompany){
            return new OkObjectResult(await repo.getAll(idCompany));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id){
            return new OkObjectResult(await repo.WithId(id));
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceReq req){
            var entry = req.toEntity(HttpContext.UserId().Value);
            await repo.Create(entry);
            return Created($"/api/entrys/${entry.Id}", entry);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var entry = await repo.WithId(id);
            await repo.Delete(entry);
            return new OkObjectResult(entry);
        }
    }
}