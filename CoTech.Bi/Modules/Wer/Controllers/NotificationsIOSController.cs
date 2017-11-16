using System;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Wer.Models.Requests;
using CoTech.Bi.Modules.Wer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Wer.Controllers
{
    [Route("/api/companies/{idCompany}/res")]
    public class NotificationsIOSController : Controller
    {
        private NotificationsIOSRepository _repository;

        public NotificationsIOSController(NotificationsIOSRepository repo)
        {
            this._repository = repo;
        }
        [HttpPost("token")]
        [RequiresRole(WerRoles.Ceo,WerRoles.Director,WerRoles.Operator)]
        public async Task<IActionResult> Create([FromRoute(Name = "idCompany")] long idCompany,[FromBody]TokenRequest token)
        {
            try
            {
                var user = HttpContext.UserId().Value;
                var tkn = _repository.Create(user, token.device);
                if (tkn != null)
                {
                    return new ObjectResult(tkn) {StatusCode = 201};
                }
                else
                {
                    return new ObjectResult(new {message = "Ya tienes ese dispositivo registrado."}) {StatusCode = 304};
                }
            }
            catch (Exception e)
            {
                return new ObjectResult(new {message = e.Message}){StatusCode = 500};
            }
        }
    }
}