using Microsoft.AspNetCore.Mvc;
using PJATKInżynierka.Services;

namespace PJATKInżynierka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateDeliveryController : ControllerBase
    {
        private readonly IDateDeliveryDatabaseService _dbService;

        public DateDeliveryController(IDateDeliveryDatabaseService dbService)
        {
            _dbService = dbService;
        }

        [Route("{date}")]
        [HttpGet]
        public async Task<IActionResult> GetDeliveries(DateTime date)
        {
            var exports = await _dbService.GetDeliveries(date);

            return Ok(exports);
        }
    }
}
