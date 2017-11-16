using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoTech.Bi.Modules.Wer.Models;
using  CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Authorization;
using CoTech.Bi.Util;
using CoTech.Bi.Core.Permissions.Repositories;
using CoTech.Bi.Modules.Wer.Repositories;

namespace CoTech.Bi.Modules.Wer.Controllers
{
    [Route("/api/res/weeks")]
    public class WeekController : Controller
    {
        
        private readonly WeekRepository _weekRepository;
        
        private readonly PermissionRepository _permissionRepository;
        
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="repository"></param>
        public WeekController(WeekRepository weekRepository,PermissionRepository permissionRepository)
        {
            this._weekRepository = weekRepository;
            this._permissionRepository = permissionRepository;
        }
        
        [HttpGet("pages/{page}")]
        [RequiresAuth]
        public async Task<IActionResult> GetAll(int? page)
        {
            try
            {
                var returnValue = await _weekRepository.paginateWeeks(page);
                
                return new OkObjectResult(returnValue);
            }
            catch (Exception e)
            {
                return new ObjectResult(new {error = e.Message}){StatusCode = 500};
            }
        }
        
        [HttpGet]
        [RequiresAuth]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var returnValue = await _weekRepository.paginateWeeks(1);
                
                return new OkObjectResult(returnValue);
            }
            catch (Exception e)
            {
                return new ObjectResult(new {error = e.Message}){StatusCode = 500};
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> WithId(long id) {
            var week = await _weekRepository.withId(id);
            return Ok(week);
        }
    }
}