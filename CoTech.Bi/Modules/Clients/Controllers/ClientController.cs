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

        [HttpPost]
        [RequiresRoot]
        public async Task<IActionResult> Create([FromBody] CreateClientReq req){
            var client = req.toEntity();
            await clientRepo.Create(client);
            return Created($"/api/clients/${client.Id}", client);
        }
    }
}