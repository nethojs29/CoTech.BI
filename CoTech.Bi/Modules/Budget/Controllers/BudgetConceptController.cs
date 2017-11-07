using System;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Budget.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Budget.Controllers{
    [Route("api/companies/{idCompany}/budgets/{idBudget}")]
    public class BudgetConceptController : Controller{
        private readonly BudgetConceptRepository repo;

        public BudgetConceptController(BudgetConceptRepository repo){
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> getAllByBudgetIdbud(long idBudget){
            return new OkObjectResult(await repo.getAllByBudget(idBudget));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBudgetConceptReq req){
            var concept = req.toEntity(HttpContext.UserId().Value);
            await repo.Create(concept);
            return Created($"/api/budgets/${concept.Id}", concept);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateBudgetConceptReq req){
            var result = await repo.Update(id, req);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            Console.WriteLine(id);
            var budget = await repo.WithId(id);
            Console.WriteLine(budget);
            await repo.Delete(budget);
            return new OkObjectResult(budget);
        }
        
        
    }
}