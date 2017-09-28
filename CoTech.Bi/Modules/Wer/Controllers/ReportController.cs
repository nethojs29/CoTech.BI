using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Wer.Models;
using CoTech.Bi.Modules.Wer.Models.Entities;
using CoTech.Bi.Modules.Wer.Models.Requests;
using CoTech.Bi.Modules.Wer.Repositories;
using CoTech.Bi.Util;
using Microsoft.AspNetCore.Http;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Wer.Models.Responses;
using Microsoft.Azure.KeyVault.Models;

namespace CoTech.Bi.Modules.Wer.Controllers
{
    [Route("/api/companies/{idCompany}/res")]
    [RequiresRole(WerRoles.Ceo,WerRoles.Director,WerRoles.Operator)]
    public class ReportController : Controller
    {

        private readonly ReportRepository _reportRepository;
        
        private readonly FilesRepository _filesRepository;

        public ReportController(ReportRepository reportRepository,FilesRepository filesRepository)
        {
            this._reportRepository = reportRepository;
            this._filesRepository = filesRepository;
        }
        [HttpGet("companies")]
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
        [HttpGet("reports/search/{idUser}/{idWeek}")]
        public async Task<IActionResult> SearchOrCreate(long idCompany, long idUser, long idWeek)
        {
            try
            {
                long idCreator = long.Parse(HttpContext.UserId().ToString());
                var report =
                    await _reportRepository.SearchOrCreate(idCompany, idUser, idWeek,idCreator);
                var reportResponse = new ReportResponse(report);
                return new ObjectResult(reportResponse){StatusCode = 200};
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

        [HttpGet("reports/{idReport}/files/{idFile}")]
        public async Task<IActionResult> DownloadFileReport([FromQuery(Name = "idFile")] long idFile)
        {
            try
            {
                var fileEntity = await _filesRepository.ById(idFile);
                if (fileEntity != null)
                {
                    var stream = System.IO.File.ReadAllBytes(fileEntity.Uri);
                    var response = File(stream, fileEntity.Mime);
                    return response;
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch (Exception e)
            {
                return new ObjectResult(new {message = e.Message}){StatusCode = 500};
            }
        }

        [HttpPut("reports/{idReport}/files")]
        public async Task<IActionResult> UploadFileReport([FromQuery(Name = "idReport")] long idReport,
            [FromForm(Name = "file")] IFormFile formFile)
        {
            try
            {
                if (formFile != null)
                {
                    var directory = Directory.GetCurrentDirectory()+ "/storage/wer/";
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    var filePath = directory + Guid.NewGuid() + Path.GetExtension(formFile.FileName);
                    if (formFile.Length > 0)
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                    var file = await _filesRepository.CreateFile(new FileEntity()
                    {
                        Mime = MimeReader.GetMimeType(Path.GetExtension(formFile.FileName)),
                        Name = formFile.FileName,
                        Uri = filePath,
                        ReportId = idReport
                    });
                    if (file != null)
                    {
                        return new ObjectResult(file){StatusCode = 201};
                    }
                    else
                    {
                        System.IO.File.Delete(filePath);
                        return StatusCode(500);
                    } 
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