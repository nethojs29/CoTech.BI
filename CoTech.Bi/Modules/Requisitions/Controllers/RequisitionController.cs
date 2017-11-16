using System;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Requisitions.Models;
using CoTech.Bi.Modules.SmallBox;
using CoTech.Bi.Modules.SmallBox.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Requisitions.Controllers{
    [Route("api/companies/{idCompany}/requisitions")]
    public class RequisitionController :Controller{
        private readonly RequisitionRepository requisitionRepo;
        private readonly SmallBoxRepository smallRepo;

        public RequisitionController(RequisitionRepository requisitionRepo, SmallBoxRepository smallRepo){
            this.requisitionRepo = requisitionRepo;
            this.smallRepo = smallRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> getAll(){
            return new OkObjectResult(await requisitionRepo.getAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id){
            return new OkObjectResult(await requisitionRepo.WithId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRequisitionReq req){
            var requisition = req.toEntity(HttpContext.UserId().Value);
            await requisitionRepo.Create(requisition);
            return Created($"/api/requisitions/${requisition.Id}", requisition);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateRequisitionReq req){
            var result = await requisitionRepo.Update(id, req);
            return Ok(result);
        }

        [HttpPut("{id}/approve")]
        public async Task<IActionResult> Approve(long id, [FromBody] ApproveRequisitionReq req){
            var requisition = await requisitionRepo.WithId(id);
            requisition.PaymentMethod = req.PaymentMethod;
            requisition.ApproveUserId = HttpContext.UserId();
            requisition.ApproveDate = DateTime.Now;
            requisition.Status = 2;
            await requisitionRepo.Approve(requisition);
            var mov = new SmallBoxEntity {
                Concept = String.Format("Pago de Requisición {0}", requisition.Keyword),
                Amount = requisition.Total,
                Date = DateTime.Now,
                Type = 0,
                RequisitionId = requisition.Id,
                CreatedAt = DateTime.Now,
                CompanyId = requisition.CompanyId,
                CreatorId = HttpContext.UserId().Value
            };
            await smallRepo.Create(mov);
            return new OkObjectResult(requisition);
        }

        [HttpPut("{id}/comprobate")]
        public async Task<IActionResult> Comprobate(long id, [FromBody] ComprobateRequisitionReq req,
            [FromForm(Name = "file")] IFormFile formFile){
            var requisition = await requisitionRepo.WithId(id);
            //No sé cómo subir archivos (8

            try {
                if (formFile != null) {
                    var directory = Directory.GetCurrentDirectory();
                    var filePath = directory + Guid.NewGuid() + Path.GetExtension(formFile.FileName);
                    if (formFile.Length > 0) {
                        using (var stream = new FileStream(filePath, FileMode.Create)) {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                    requisition.Refund = req.Refund;
                    requisition.ComprobateFileUrl = filePath;
                    requisition.ComprobateDate = DateTime.Now;
                    requisition.ComprobateUserId = HttpContext.UserId();
            
                    return new OkObjectResult(requisition);        
                } else {
                    return new BadRequestResult();
                }
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                return StatusCode(500);
            }
            
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var requisition = await requisitionRepo.WithId(id);
            await requisitionRepo.Delete(requisition);
            return new OkObjectResult(requisition);
        }

    }
}