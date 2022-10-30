using Application.Services.DateDelivery;
using Microsoft.AspNetCore.Mvc;

namespace PJATKInżynierka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryDatabaseService _dbService;

        public DeliveryController(IDeliveryDatabaseService dbService)
        {
            _dbService = dbService;
        }

        [Route("{date}")]
        [HttpGet]
        public async Task<IActionResult> GetDeliveries(DateTime date)
        {
            var deliveries = await _dbService.GetDeliveries(date);

            return Ok(deliveries);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeliveries()
        {
            var deliveries = await _dbService.GetDeliveries();

            return Ok(deliveries);
        }

        [Route("{date}")]
        [HttpGet]
        public async Task<IActionResult> AddDelivery(DateTime date)
        {
            var deliveries = await _dbService.GetDeliveries(date);

            return Ok(deliveries);
        }
    }
}
