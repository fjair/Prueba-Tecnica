using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Entities;
using TaskManager.API.Services;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]/[action]")]    
    [ApiController]
    public class TaaskController : ControllerBase
    {
        ITaaskService taaskService;
        public TaaskController(ITaaskService taaskService)
        {
            this.taaskService = taaskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Taask> result = null;
            result = await taaskService.GetAllTaasks();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetByID(int id)
        {
            Taask result = null;
            result = await taaskService.GetTaaksByID(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Taask model)
        {          
            bool result = await taaskService.AddTaask(model);

            if (!result)
                return BadRequest();

            return Ok(result);
        }
        
        
        [HttpPut]
        public async Task<IActionResult> Update(Taask model)
        {          
            bool result = await taaskService.UpdateTaask(model);

            if (!result)
                return BadRequest();

            return Ok(result);
        }
        
        [HttpPut]
        public async Task<IActionResult> Delete(Taask model)
        {          
            bool result = await taaskService.DeleteTaask(model);

            if (!result)
                return BadRequest();

            return Ok(result);
        }
    }
}
