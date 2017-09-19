using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Wer.Models;
using CoTech.Bi.Modules.Wer.Models.Requests;
using CoTech.Bi.Modules.Wer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.KeyVault.Models;

namespace CoTech.Bi.Modules.Wer.Controllers
{
    [Route("/api/companies/{idCompany}/res")]
    public class ReportController : Controller
    {

        private readonly ReportRepository _reportRepository;

        public ReportController(ReportRepository reportRepository)
        {
            this._reportRepository = reportRepository;
        }

        [HttpGet("companies/all")]
        public async Task<IActionResult> getAllCompanies(long idCompany)
        {
            try
            {
                var result = _reportRepository.GetCompaniesRecursive(idCompany);
                return new OkObjectResult(new {companies = result});
            }
            catch (Exception e)
            {
                return new ObjectResult(new {error = e.Message}){StatusCode = 500};
            }
        }
        
        [HttpGet("reports/week/{idWeek}")]
        public async Task<IActionResult> byWeek(long idCompany, long? idWeek)
        {
            try
            {
                return new OkObjectResult(await _reportRepository.byWeekRecursive(idCompany,idWeek));
            }
            catch (Exception e)
            {
                return new ObjectResult(new {error = e.Message}) {StatusCode = 500};
            }
            
        }

        [HttpGet("reports/user/{idUser}")]
        public async Task<IActionResult> byUser(int? idUser)
        {
            try
            {
                if (idUser == null)
                {
                    var userCurrent = HttpContext.UserId();
                    var returnData = await _reportRepository.byUser((int) userCurrent);
                    return new OkObjectResult(returnData);
                }
                else
                {
                    var returnData = await _reportRepository.byUser((int) idUser);
                    return new OkObjectResult(returnData);

                }
            }
            catch (Exception e)
            {
                return new ObjectResult(new {error = e.Message}) {StatusCode = 500};
            }
        }

        [HttpPost("reports")]
        public async Task<IActionResult> CreateReport([FromBody] ReportRequest request)
        {
            try
            {
                return new OkObjectResult(await _reportRepository.Create(request));
            }
            catch (Exception e)
            {
                return new ObjectResult(new {error = e.Message}){StatusCode = 500};
            }
        }

        [HttpPost("reports/{idReport}/files")]
        public async Task<IActionResult> UploadFileReport([FromQuery(Name = "idReport")] long idReport,
            [FromForm(Name = "file")] IFormFile formFile)
        {
            try
            {
                if (formFile != null)
                {
                    var directory = Directory.GetCurrentDirectory();
                    var filePath = directory + Guid.NewGuid() + Path.GetExtension(formFile.FileName);
                    if (formFile.Length > 0)
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                    return Ok(new { count = formFile.Length, formFile.ContentType});
                }else 
                    return new BadRequestResult();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode(500); // 500 is generic server error
            }
        }

        [HttpDelete("reports/{idReport}")]
        public async Task<IActionResult> DeleteReport(long idReport)
        {
            try
            {
                return new OkObjectResult(new {deleted = _reportRepository.Delete(idReport)});
            }
            catch (Exception e)
            {
                return new ObjectResult(new {error = e.Message}){StatusCode = 500};
            }
        }

        [HttpGet("reports/{idReport}")]
        public async Task<IActionResult> byIdReport(long idReport)
        {
            try
            {
                return new OkObjectResult(new {report = await _reportRepository.byIdReport(idReport)});
            }
            catch (Exception e)
            {
                return new ObjectResult(new {error = e.Message}) {StatusCode = 500};
            }
        }

        [HttpGet("reports/start/{idStart}/end/{idEnd}")]
        public async Task<IActionResult> filterBetweenWeeks(long idStart, long idEnd)
        {
            try
            {
                var result = _reportRepository.FilterBetweenWeeks(idStart, idEnd);
                if (result == null)
                    return new ObjectResult(new {reports = result}){StatusCode = 404};
                return new OkObjectResult(new {reports = result});
            }
            catch (Exception e)
            {
                return new ObjectResult(new {error = e.Message}) {StatusCode = 500};
            }
        }
    }
}