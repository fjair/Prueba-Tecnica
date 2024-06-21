using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Entities;
using TaskManager.API.Services;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        IGoalService service;
        public GoalController(IGoalService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Goal> result = null;
            result = await service.GetAllGoals();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetByID(int id)
        {
            Goal result = null;
            result = await service.GetGoalByID(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Goal model)
        {
            bool result = await service.AddGoal(model);

            if (!result)
                return BadRequest();

            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> Update(Goal model)
        {
            bool result = await service.UpdateGoal(model);

            if (!result)
                return BadRequest();

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Delete(Goal model)
        {
            bool result = await service.DeleteGoal(model);

            if (!result)
                return BadRequest();

            return Ok(result);
        }
    }
}
