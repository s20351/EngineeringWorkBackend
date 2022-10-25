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

        [Route("AddOrderHatchery/{farmId}")]
        [HttpPost]
        public async Task<IActionResult> AddOrderHatchery(AddOrderHatcheryDTO orderHatchery, int farmId)
        {
            await _dbService.AddOrderHatchery(orderHatchery, farmId);

            return Ok();
        }
    }
}
