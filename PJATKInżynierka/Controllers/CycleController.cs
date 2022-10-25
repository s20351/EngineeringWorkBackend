using Application.Services.Cycles;
using Microsoft.AspNetCore.Mvc;
using PJATKInżynierka.DTOs.CyclesDTOs;

namespace PJATKInżynierka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CycleController : ControllerBase
    {
        private readonly ICycleDatabaseService _dbService;

        public CycleController(ICycleDatabaseService dbService)
        {
            _dbService = dbService;
        }

        [Route("CreateCycle/{farmId}")]
        [HttpPost]
        public async Task<IActionResult> CreateCycle(CreateCycleDTO cycle, int farmId)
        {
            await _dbService.CreateCycle(cycle, farmId);

            return Ok();
        }

        [Route("{farmId}")]
        [HttpGet]
        public async Task<IActionResult> GetCycles(int farmId)
        {
            var cycles = await _dbService.GetCycles(farmId);

            return Ok(cycles);
        }
    }
}
