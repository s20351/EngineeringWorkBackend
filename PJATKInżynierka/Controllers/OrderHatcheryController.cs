using Application.Services.OrdersHatchery;
using Microsoft.AspNetCore.Mvc;
using PJATKInżynierka.DTOs.OrderHatcheryDTOs;

namespace PJATKInżynierka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHatcheryController : ControllerBase
    {
        private readonly IOrderHatcheryDatabaseService _dbService;

        public OrderHatcheryController(IOrderHatcheryDatabaseService dbService)
        {
            _dbService = dbService;
        }

        [Route("{farmId}")]
        [HttpPost]
        public async Task<IActionResult> AddOrderHatchery(AddOrderHatcheryDTO orderHatchery, int farmId)
        {
            await _dbService.AddOrderHatchery(orderHatchery, farmId);

            return Ok();
        }

        [Route("{farmId}")]
        [HttpGet]
        public async Task<IActionResult> GetOrdersHatchery(int farmId)
        {
            var deliveries = await  _dbService.GetOrdersHatchery(farmId);

            return Ok(deliveries);
        }

        [Route("GetDeliveriesDates/{farmId}")]
        [HttpGet]
        public async Task<IActionResult> GetDeliveriesDates(int farmId)
        {
            var deliveries = await _dbService.GetDeliveriesDates(farmId);

            return Ok(deliveries);
        }
    }
}
