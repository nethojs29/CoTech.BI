using System;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Personal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Personal.Controllers{
    [Route("api/companies/{idCompany}/personal")]
    public class PersonalController:Controller{
        private readonly PersonalRepository personalRepo;

        public PersonalController(PersonalRepository PersonalRepo){
            this.personalRepo = PersonalRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> getAll(){
            return new OkObjectResult(await personalRepo.getAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id){
            return new OkObjectResult(await personalRepo.WithId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonalReq req){
            Console.WriteLine(req);
            var personal = req.toEntity(HttpContext.UserId().Value);
            await personalRepo.Create(personal);
            return Created($"/api/personal/${personal.Id}", personal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdatePersonalReq req){
            var result = await personalRepo.Update(id, req);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var personal = await personalRepo.WithId(id);
            await personalRepo.Delete(personal);
            return new OkObjectResult(personal);
        }
    }
}