using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.DinningRooms.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoTech.Bi.Modules.DinningRooms.Controllers{
    [Route("api/dinningRooms")]
    public class DinningRoomController : Controller{
        private readonly DinningRoomRepository dinningRepo;

        public DinningRoomController(DinningRoomRepository dinningRepo){
            this.dinningRepo = dinningRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> getAll(){
            return new OkObjectResult(await dinningRepo.getAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id){
            return new OkObjectResult(await dinningRepo.WithId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDinningRoomReq req){
            var dinningRoom = req.toEntity(HttpContext.UserId().Value);
            await dinningRepo.Create(dinningRoom);
            return Created($"/api/dinningRooms/${dinningRoom.Id}", dinningRoom);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateDinningRoomReq req){
            var result = await dinningRepo.Update(id, req);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id){
            var dinningRoom = await dinningRepo.WithId(id);
            await dinningRepo.Delete(dinningRoom);
            return new OkObjectResult(dinningRoom);
        }
    }
}