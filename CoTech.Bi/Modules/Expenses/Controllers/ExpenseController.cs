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

        [HttpGet("requisition/{requisitionId}")]
        public async Task<IActionResult> getAllByRequisition(long requisitionId){
            return new OkObjectResult(await expenseRepo.getAllByRequisition(requisitionId));
        }

        [HttpGet("type/{typeId}/{year}")]
        public async Task<IActionResult> getAllByTypeInYear(long typeId, int year){
            return new OkObjectResult(await expenseRepo.getAllApprovedExpensesByTypeInYear(typeId, year));
        }

        [HttpGet("type/{typeId}/{year}/{month}")]
        public async Task<IActionResult> getAllByTypeInMonth(long typeId, int year, int month){
            return new OkObjectResult(await expenseRepo.getAllByTypeInMonth(typeId, year, month));
        }

        [HttpGet("group/{groupId}/{year}")]
        public async Task<IActionResult> getAllByGroupInYear(long groupId, int year){
            return new OkObjectResult(await expenseRepo.getAllApprovedExpensesByGroupInYear(groupId, year));
        }

        [HttpGet("group/{groupId}/{year}/{month}")]
        public async Task<IActionResult> getAllByGroupInMonth(long groupId, int year, int month){
            return new OkObjectResult(await expenseRepo.getAllByGroupInMonth(groupId, year, month));
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