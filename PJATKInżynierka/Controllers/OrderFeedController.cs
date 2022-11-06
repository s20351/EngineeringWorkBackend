using Application.Services.OrdersFeed;
using Microsoft.AspNetCore.Mvc;
using PJATKInżynierka.DTOs.OrderFeedDTOs;

namespace PJATKInżynierka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderFeedController : ControllerBase
    {
        private readonly IOrderFeedDatabaseService _dbService;

        public OrderFeedController(IOrderFeedDatabaseService dbService)
        {
            _dbService = dbService;
        }

        [Route("{farmId}")]
        [HttpPost]
        public async Task<IActionResult> AddOrderFeed(AddOrderFeedDTO orderHatchery, int farmId)
        {
            await _dbService.AddOrderFeed(orderHatchery, farmId);

            return Ok();
        }

        [Route("{farmId}")]
        [HttpGet]
        public async Task<IActionResult> GetOrdersFeed(int farmId)
        {
            var deliveries = await _dbService.GetOrdersFeed(farmId);

            return Ok(deliveries);
        }

        [Route("GetDeliveriesDates/{farmId}")]
        [HttpGet]
        public async Task<IActionResult> GetDeliveriesDates(int farmId)
        {
            var deliveries = await _dbService.GetDeliveriesDates(farmId);

            return Ok(deliveries);
        }

        [Route("GetOrdersSchedule/{farmerId}")]
        [HttpGet]
        public async Task<IActionResult> GetOrdersSchedule(int farmerId)
        {
            var schedule = await _dbService.GetOrdersSchedule(farmerId);

            return Ok(schedule);
        }

        [Route("GetEvents/{farmerId}")]
        [HttpGet]
        public async Task<IActionResult> GetFeedFarmerEvents(int farmerId)
        {
            var objectInfo = await _dbService.GetFeedFarmerEvents(farmerId);

            return Ok(objectInfo);
        }
    }
}
