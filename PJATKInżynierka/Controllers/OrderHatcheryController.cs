using Microsoft.AspNetCore.Mvc;
using PJATKInżynierka.DTOs.OrderHatcheryDTOs;
using PJATKInżynierka.Services;

namespace PJATKInżynierka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OderHatcheryController : ControllerBase
    {
        private readonly IOrderHatcheryDatabaseService _dbService;

        public OderHatcheryController(IOrderHatcheryDatabaseService dbService)
        {
            _dbService = dbService;
        }

        [Route("AddOrderHatchery/{farmId}")]
        [HttpPost]
        public async Task<IActionResult> AddOrderHatchery(AddOrderHatcheryDTO orderHatchery, int farmId)
        {
            await _dbService.AddOrderHatchery(orderHatchery, farmId);

            return Ok();
        }
    }
}
