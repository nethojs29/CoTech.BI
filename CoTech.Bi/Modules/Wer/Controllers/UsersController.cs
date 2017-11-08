using System;
using System.Collections.Generic;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Wer.Models.Responses;
using CoTech.Bi.Modules.Wer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.Wer.Controllers
{
    [Route("/api/companies/{idCompany}/res")]
    public class UsersController : Controller
    {
        
        
        private UsersRepository _repository;

        public UsersController(UsersRepository _repository)
        {
            this._repository = _repository;
        }
        
        [HttpGet("users")]
        [RequiresRole(WerRoles.Ceo, WerRoles.Director, WerRoles.Operator)]
        public IActionResult GetUsers(long idCompany)
        {
            try
            {
                var users = _repository.GetUsersByCompany(idCompany, new List<WerUserAndPermissions>());
                return new ObjectResult(users);
            }
            catch (Exception e)
            {
                return new ObjectResult(new {message = e.Message}){StatusCode = 500};
            }
        }
    }
}