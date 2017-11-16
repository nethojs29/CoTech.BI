using System.Net;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Expenses.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Expenses.Controllers{
    [Route("api/companies/{idCompany}/expenses")]
    public class ExpenseController : Controller{
        private readonly ExpenseRepository expenseRepo;

        public ExpenseController(ExpenseRepository expenseRepo){
            this.expenseRepo = expenseRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> getAll(){
            return new OkObjectResult(await expenseRepo.getAll());
        }

        [HttpGet("requisition/{requisitionId}")]
        public async Task<IActionResult> getAllByRequisition(long requisitionId){
            return new OkObjectResult(await expenseRepo.getAllByRequisition(requisitionId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id){
            return new OkObjectResult(await expenseRepo.WithId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExpenseReq req){
            var expense = req.toEntity();
            await expenseRepo.Create(expense);
            return Created($"/api/expenses/${expense.Id}", expense);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateExpenseReq req){
            var result = await expenseRepo.Update(id, req);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var provider = await expenseRepo.WithId(id);
            await expenseRepo.Delete(provider);
            return new OkObjectResult(provider);
        }
    }
    
}