using System;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Core.Companies.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Core.Companies.Models{
    [Route("api/companies/{idCompany}/departments")]
    public class DepartmentController:Controller{
        private readonly DepartmentRepository departmentRepo;

        public DepartmentController(DepartmentRepository departmentRepo){
            this.departmentRepo = departmentRepo;
        }
        
        [HttpGet("{id}")]
        [RequiresAnyRole]
        public async Task<IActionResult> GetById(long id) {
            return new OkObjectResult(await departmentRepo.WithId(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            return new OkObjectResult(await departmentRepo.getAll());
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentReq req){
            var department = req.toEntity();
            await departmentRepo.Create(department);
            return Created($"/api/departments/${department.Id}", department);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateDepartmentReq req) {
            var updateCmd = new UpdateDepartmentCmd(id, req, HttpContext.UserId().Value);
            var department = await departmentRepo.Update(updateCmd);
            return Ok(department);
        }
        
        [HttpDelete("{id}")]
        [RequiresRoot]
        public async Task<IActionResult> Delete(long id){
            var cmd = new DeleteDepartmentCmd(id, HttpContext.UserId().Value);
            var department = await departmentRepo.Delete(cmd);
            return Ok(department);
        }
    }
}