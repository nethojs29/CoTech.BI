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
        private readonly BudgetConceptRepository conceptRepo;
        private readonly ExpenseGroupRepository groupRepo;

        public BudgetController(BudgetRepository budgetRepo, ExpenseRepository expenseRepo, BudgetConceptRepository conceptRepo, ExpenseGroupRepository groupRepo){
            this.budgetRepo = budgetRepo;
            this.expenseRepo = expenseRepo;
            this.conceptRepo = conceptRepo;
            this.groupRepo = groupRepo;
        }
        
        [HttpGet("{year}")]
        public async Task<IActionResult> getAll(long year, long idCompany){
            return new OkObjectResult(await budgetRepo.getFromYear(year, idCompany));
        }

        [HttpGet("{year}/{month}/{dinningRoom}")]
        public async Task<IActionResult> monthly(long year, long month, long dinningRoom, long idCompany){
            return new OkObjectResult(await budgetRepo.monthly(year, month, dinningRoom, idCompany));
        }

        [HttpGet("limit/{year}/{month}")]
        public async Task<IActionResult> getMonthLimit(int year, int month, long idCompany){
            var budgetArray = await budgetRepo.monthlyBudget(year, month, idCompany);
            var totalBudget = budgetArray.Count > 0 ? budgetArray.Select(b => b.Amount).Aggregate((a, b) => a + b) : 0;
            var spent = await expenseRepo.getAllExpensesInMonth(year, month);
            var totalSpent = 0.0;
            spent.ForEach(e => {
                totalSpent += e.Price * e.Quantity;
            });
            Console.WriteLine(totalBudget-totalSpent);
            return new OkObjectResult(totalBudget - totalSpent);

        }

        [HttpGet("limit/{year}/{month}/{groupId}")]
        public async Task<IActionResult> getMonthGroupLimit(int year, int month, long groupId, long idCompany){
            var budgets = await conceptRepo.monthlyGroupBudget(groupId, idCompany, month, year);
            var group = await groupRepo.WithId(groupId);
            var totalBudget = 0.0;
            if(budgets.Count > 0){
                totalBudget = budgets.Select(b => b.Amount).Aggregate((a, b) => a + b);
            } else {
                var budget = await budgetRepo.getBudgetByType(idCompany, group.TypeId);
                var concepts = await conceptRepo.getAllByBudget(budget.Id);
                return new OkObjectResult(budget.Amount - concepts.Select(c => c.Amount).Aggregate((a, b) => a+b));
            }
             
            var spent = await expenseRepo.getAllExpensesByGroupInMonth(groupId, year, month);
            var totalSpent = 0.0;
            spent.ForEach(e => {
                totalSpent += e.Price * e.Quantity;
            });
            Console.WriteLine(totalBudget - totalSpent);
            return new OkObjectResult(totalBudget - totalSpent);
        }

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