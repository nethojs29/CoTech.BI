using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Wer.Models;
using CoTech.Bi.Modules.Wer.Models.Requests;
using CoTech.Bi.Modules.Wer.Repositories;

namespace CoTech.Bi.Modules.Wer.Controllers
{
    [Route("/api/companies/{idCompany}/res/reports")]
    public class ReportController : Controller
    {

        private readonly ReportRepository _reportRepository;

        public ReportController(ReportRepository reportRepository)
        {
            this._reportRepository = reportRepository;
        }
        
        [HttpGet("week/{idWeek}")]
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

        [HttpGet("user/{idUser}")]
        public async Task<IActionResult> byUser(int? idUser)
        {
            try
            {
                if (idUser == null)
                {
                    var userCurrent = HttpContext.UserId();
                    return new OkObjectResult(_reportRepository.byUser((int)userCurrent));
                }
                else
                {
                    return new OkObjectResult(_reportRepository.byUser((int)idUser));

                }
            }
            catch (Exception e)
            {
                return new ObjectResult(new {error = e.Message}) {StatusCode = 500};
            }
        }

        [HttpPost]
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

        [HttpDelete("{idReport}")]
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

        [HttpGet("{idReport}")]
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

        [HttpGet("start/{idStart}/end/{idEnd}")]
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