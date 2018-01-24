using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Invoices.Repositories;
using CoTech.Bi.Modules.Invoices.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Invoices.Controllers{
    [Route("api/companies/{idCompany}/invoices/{idInvoice}/payments")]
    public class InvoicePaymentController : Controller{
        private readonly InvoicePaymentRepository repo;

        public InvoicePaymentController(InvoicePaymentRepository repo){
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> getAll(long idCompany, long idInvoice){
            return new OkObjectResult(await repo.getAllByInvoice(idCompany, idInvoice));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id){
            return new OkObjectResult(await repo.WithId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInvoicePaymentReq req){
            var entry = req.toEntity(HttpContext.UserId().Value);
            await repo.Create(entry);
            return Created($"/api/invoice/payments/${entry.Id}", entry);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateInvoicePaymentReq req){
            await repo.Update(id, req);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var pay = await repo.WithId(id);
            await repo.Delete(pay);
            return new OkObjectResult(pay);
        }
    }
}