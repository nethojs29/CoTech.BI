﻿using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Expenses.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Expenses.Controllers{
    [Route("api/companies/{idCompany}/expenseTypes")]
    public class ExpenseTypeController : Controller {
        private readonly ExpenseTypeRepository expenseRepo;

        public ExpenseTypeController(ExpenseTypeRepository expenseRepo){
            this.expenseRepo = expenseRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> getAll(long idCompany){
            return new OkObjectResult(await expenseRepo.getAll(idCompany));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id){
            return new OkObjectResult(await expenseRepo.WithId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExpenseTypeReq req){
            var expense = req.toEntity(HttpContext.UserId().Value);
            await expenseRepo.Create(expense);
            return Created($"/api/expenses/${expense.Id}", expense);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateExpenseTypeReq req){
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