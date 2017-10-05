using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Banks.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Clients.Controllers{
    [Route("/api/companies/{idCompany}/banks")]
    public class BankController:Controller{
        private readonly BankRepository bankRepo;

        public BankController(BankRepository bankRepo){
            this.bankRepo = bankRepo;
        }

        [HttpGet]
        public async Task<IActionResult> getAll(long idCompany){
            return new OkObjectResult(await bankRepo.getAll(idCompany));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id){
            return new OkObjectResult(await bankRepo.WithId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBankReq req){
            var bank = req.toEntity(HttpContext.UserId().Value);
            await bankRepo.Create(bank);
            return Created($"/api/banks/${bank.Id}", bank);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateBankReq req){
            var result = await bankRepo.Update(id, req);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var bank = await bankRepo.WithId(id);
            await bankRepo.Delete(bank);
            return new OkObjectResult(bank);
        }
    }
}