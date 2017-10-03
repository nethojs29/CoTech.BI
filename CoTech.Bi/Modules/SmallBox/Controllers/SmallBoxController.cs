using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.SmallBox.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.SmallBox.Controllers{
    [Route("api/companies/{idCompany}/smallbox")]
    public class SmallBoxController:Controller{
        private readonly SmallBoxRepository smallboxRepo;

        public SmallBoxController(SmallBoxRepository provierRepo){
            this.smallboxRepo = provierRepo;
        }

        [HttpGet]
        public async Task<IActionResult> getAll(){
            return new OkObjectResult(await smallboxRepo.getAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id){
            return new OkObjectResult(await smallboxRepo.WithId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSmallBoxEntryReq req){
            var entry = req.toEntity(HttpContext.UserId().Value);
            await smallboxRepo.Create(entry);
            return Created($"/api/entrys/${entry.Id}", entry);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateSmallBoxEntryReq req){
            var result = await smallboxRepo.Update(id, req);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var entry = await smallboxRepo.WithId(id);
            await smallboxRepo.Delete(entry);
            return new OkObjectResult(entry);
        }
    }
}