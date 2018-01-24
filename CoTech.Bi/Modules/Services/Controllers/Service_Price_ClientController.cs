﻿using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Services.Controllers{
    [Route("api/companies/{idCompany}/services/prices")]
    public class Service_Price_ClientController:Controller{
        private readonly Service_Price_ClientRepository spcRepo;

        public Service_Price_ClientController(Service_Price_ClientRepository spcRepo){
            this.spcRepo = spcRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> getAll(long idCompany){
            return new OkObjectResult(await spcRepo.getAll(idCompany));
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetById(long clientId){
            return new OkObjectResult(await spcRepo.WithClientId(clientId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServicePriceClientReq req){
            var service = req.toEntity(HttpContext.UserId().Value);
            await spcRepo.Create(service);
            return Created($"/api/services/${service.Id}", service);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateServicePriceClientReq req){
            var result = await spcRepo.Update(id, req);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var service = await spcRepo.WithId(id);
            await spcRepo.Delete(service);
            return new OkObjectResult(service);
        }
    }
}