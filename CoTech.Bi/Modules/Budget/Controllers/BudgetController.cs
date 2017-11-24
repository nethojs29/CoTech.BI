using System;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Budget.Models;
using CoTech.Bi.Modules.Expenses.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Budget.Controllers{
    [Route("api/companies/{idCompany}/budgets")]
    public class BudgetController : Controller{
        private readonly BudgetRepository budgetRepo;
        private readonly ExpenseRepository expenseRepo;

        public BudgetController(BudgetRepository budgetRepo, ExpenseRepository expenseRepo){
            this.budgetRepo = budgetRepo;
            this.expenseRepo = expenseRepo;
        }
        
        [HttpGet("{year}")]
        public async Task<IActionResult> getAll(long year, long idCompany){
            return new OkObjectResult(await budgetRepo.getFromYear(year, idCompany));
        }

        [HttpGet("{year}/{month}")]
        public async Task<IActionResult> monthly(long year, long month, long idCompany){
            return new OkObjectResult(await budgetRepo.monthly(year, month, idCompany));
        }
        
//        [HttpGet("limit/{typeId}/{year}")]
//        public async Task<IActionResult> getLimit(long typeId, int year){
//            var totalBudgetExpenses = await budgetRepo.getAllByGroup(typeId, year);
//            var totalSpend = await expenseRepo.getAllApprovedExpensesByGroupInYear(typeId, year);
//
//            var t = totalBudgetExpenses.Select(b => b.Amount).Aggregate((a, b) => a + b);
//            var s = 0.0;
//            totalSpend.ForEach(e => {
//                s += e.Quantity * e.Price;
//            });
//            
//            return new OkObjectResult(t-s);
//        }

        [HttpGet("limit/{year}/{month}")]
        public async Task<IActionResult> getMonthLimit(int year, int month, long idCompany){
            var budgetArray = await budgetRepo.monthly(year, month, idCompany);
            var totalBudget = budgetArray.Select(b => b.Amount).Aggregate((a, b) => a + b);
            var spent = await expenseRepo.getAllExpensesInMonth(year, month);
            var totalSpent = 0.0;
            spent.ForEach(e => {
                totalSpent += e.Price * e.Quantity;
            });
            Console.WriteLine(totalBudget-totalSpent);
            return new OkObjectResult(totalBudget - totalSpent);

        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(long id){
//            return new OkObjectResult(await budgetRepo.WithId(id));
//        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBudgetReq req){
            var budget = req.toEntity(HttpContext.UserId().Value);
            await budgetRepo.Create(budget);
            return Created($"/api/budgets/${budget.Id}", budget);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateBudgetReq req){
            var result = await budgetRepo.Update(id, req);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var budget = await budgetRepo.WithId(id);
            await budgetRepo.Delete(budget);
            return new OkObjectResult(budget);
        }

    }
}