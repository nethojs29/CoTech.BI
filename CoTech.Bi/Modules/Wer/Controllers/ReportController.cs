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
using CoTech.Bi.Core.Users.Repositories;
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
        private readonly UserRepository _userRepository;

        public ReportController(ReportRepository reportRepository,FilesRepository filesRepository,UserRepository _repository)
        {
            this._reportRepository = reportRepository;
            this._filesRepository = filesRepository;
            this._userRepository = _repository;
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
        [HttpGet("pendings")]
        public async Task<IActionResult> getPendings(long idCompany)
        { 
            try
            {
                var idUser = HttpContext.UserId().Value;
                var result = _reportRepository.GetReportSeensRecursive(idCompany, idUser);
                return new ObjectResult(
                    new
                    {
                        pendings = result.Where(r => r.create).ToArray(),
                        unexists = result.Where(r => !r.create).ToArray()
                    })
                {
                    StatusCode = 200
                };
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
                var reportResponse = new ReportResponse(report,this._userRepository);
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
        public async Task<IActionResult> byUser(long idCompany,int? idUser)
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
        public async Task<IActionResult> CreateReport(long idCompany,[FromBody] ReportRequest request)
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
        
        [HttpPut("reports/{idReport}")]
        public async Task<IActionResult> CreateReport(long idCompany,[FromBody] ReportRequest request,long idReport)
        {
            try
            {
                var idUser = HttpContext.UserId().Value;
                var report = await _reportRepository.Update(request, idReport,idUser);
                if (report == null)
                    return NotFound();
                return new OkObjectResult(report);
            }
            catch (Exception e)
            {
                return new ObjectResult(new {error = e.Message}){StatusCode = 500};
            }
        }

        [HttpGet("reports/{idReport}/files/{idFile}")]
        public async Task<IActionResult> DownloadFileReport(long idCompany,[FromRoute(Name = "idFile")] long idFile)
        {
            try
            {
                var fileEntity = await _filesRepository.ById(idFile);
                if (fileEntity != null)
                {
                    var stream = System.IO.File.ReadAllBytes(fileEntity.Uri);
                    HttpContext.Response.ContentType = "application/pdf";
                    var response = new FileContentResult(stream, fileEntity.Mime) {
                        FileDownloadName = fileEntity.Name
                    };
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
        [HttpDelete("reports/{idReport}/files/{idFile}")]
        public async Task<IActionResult> DeleteFileReport(long idCompany,[FromRoute(Name = "idFile")] long idFile)
        {
            try
            {
                var fileEntity = _filesRepository.DeleteById(idFile);
                if (fileEntity != null)
                {
                    if (fileEntity == true)
                    {
                        return new OkObjectResult(new {message = "deleted"});
                    }
                    return new ObjectResult(new {message = "not deleted"})
                    {
                        StatusCode = 500
                    };
                }
                return new ObjectResult(new { message = "not found"})
                {
                    StatusCode = 404
                };
            }
            catch (Exception e)
            {
                return new ObjectResult(new {message = e.Message}){StatusCode = 500};
            }
        }

        [HttpPost("reports/{idReport}/{filetype}/files")]
        public async Task<IActionResult> UploadFileReport([FromRoute(Name = "idCompany")]long idCompany,[FromRoute(Name = "idReport")] long idReport,
            [FromRoute(Name = "filetype")] Int16 filetype,
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
                    var file = _filesRepository.CreateFile(new FileEntity()
                    {
                        Mime = MimeReader.GetMimeType(Path.GetExtension(formFile.FileName)),
                        Name = formFile.FileName,
                        Uri = filePath,
                        Type = filetype,
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
        public async Task<IActionResult> DeleteReport(long idCompany, long idReport)
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
        public async Task<IActionResult> byIdReport(long idCompany,long idReport)
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
        public async Task<IActionResult> filterBetweenWeeks(long idCompany,long idStart, long idEnd)
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