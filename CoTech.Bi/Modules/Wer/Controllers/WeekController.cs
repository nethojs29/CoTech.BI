using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoTech.Bi.Modules.Wer.Models;
using  CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Authorization;
using CoTech.Bi.Util;
using CoTech.Bi.Core.Permissions.Repositories;

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
        
        [HttpGet("/pages/{page}")]
        [RequiresAuth]
        public async Task<IActionResult> GetAll(int? page)
        {
            var weeks = await _weekRepository.getAll();
            var returnValue = await PaginateList<WeekEntity>.CreateAsync(weeks
                .OrderByDescending(wk => wk.EndTime)
                .ToList()
                .AsQueryable(), page ?? 1, 54);
            return new OkObjectResult(returnValue);
        }
    }
}