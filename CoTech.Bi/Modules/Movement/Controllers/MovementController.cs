using System;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Movement.Controllers{
    [Route("api/companies/{idCompany}/clients/movements")]
    public class MovementController:Controller{
        private readonly MovementRepository movementRepo;

        public MovementController(MovementRepository provierRepo){
            this.movementRepo = provierRepo;
        }

        [HttpGet]
        public async Task<IActionResult> getAll(){
            return new OkObjectResult(await movementRepo.getAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id){
            return new OkObjectResult(await movementRepo.WithId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMovementReq req){
            var movement = req.toEntity(HttpContext.UserId().Value);
            await movementRepo.Create(movement);
            return Created($"/api/movements/${movement.Id}", movement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateMovementReq req){
            Console.WriteLine(req);
            var result = await movementRepo.Update(id, req);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var movement = await movementRepo.WithId(id);
            await movementRepo.Delete(movement);
            return new OkObjectResult(movement);
        }
    }
}