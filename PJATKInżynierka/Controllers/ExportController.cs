using Microsoft.AspNetCore.Mvc;
using PJATKInżynierka.DTOs.ExportDTOs;
using PJATKInżynierka.Services;

namespace PJATKInżynierka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private readonly IExportDatabaseService _dbService;

        public ExportController(IExportDatabaseService dbService)
        {
            _dbService = dbService;
        }

        [Route("AddExport/{cycleId}")]
        [HttpPost]
        public async Task<IActionResult> AddExport(AddExportDTO export, int cycleId)
        {
            await _dbService.AddExport(export, cycleId);

            return Ok();
        }

        [Route("{cycleId}")]
        [HttpGet]
        public async Task<IActionResult> GetCycles(int cycleId)
        {
            var exports = await _dbService.GetExports(cycleId);

            return Ok(exports);
        }
    }
}
