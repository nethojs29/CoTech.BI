using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Wer.Models;
using CoTech.Bi.Modules.Wer.Repositories;

namespace CoTech.Bi.Modules.Wer.Controllers
{
    [Route("/api/res/reports")]
    public class ReportController : Controller
    {

        private readonly ReportRepository _reportRepository;

        public ReportController(ReportRepository reportRepository)
        {
            this._reportRepository = reportRepository;
        }
        
        [HttpGet("week/{idWeek}")]
        public async Task<IActionResult> byWeek( int? idWeek)
        {
            try
            {
                return new OkObjectResult(await _reportRepository.byWeek(idWeek));
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
    }
}