using System;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Modules.Clients.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Clients.Controllers{
    [Route("api/clients")]
    public class ClientController:Controller{
        private readonly ClientRepository clientRepo;

        public ClientController(ClientRepository clientRepo){
            this.clientRepo = clientRepo;
        }

        [HttpGet]
        public async Task<IActionResult> getAll(){
            return new OkObjectResult(await clientRepo.getAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id){
            return new OkObjectResult(await clientRepo.withId(id));
        }

        [HttpPost]
        [RequiresRoot]
        public async Task<IActionResult> Create([FromBody] CreateClientReq req){
            var client = req.toEntity();
            await clientRepo.Create(client);
            return Created($"/api/clients/${client.Id}", client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateClientReq req){
            var result = await clientRepo.Update(id, req);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var client = await clientRepo.withId(id);
            await clientRepo.Delete(client);
            return new OkObjectResult(client);
        }
    }
}