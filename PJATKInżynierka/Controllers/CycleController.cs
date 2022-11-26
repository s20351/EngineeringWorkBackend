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

            return StatusCode(200, "Cycle created"); ;
        }

        [Route("{farmerId}")]
        [HttpGet]
        public async Task<IActionResult> GetCycles(int farmerId)
        {
            var cycles = await _dbService.GetCycles(farmerId);

            return Ok(cycles);
        }
    }
}
