﻿using Microsoft.AspNetCore.Mvc;
using PJATKInżynierka.DTOs.OrderFeedDTOs;
using PJATKInżynierka.Services;

namespace PJATKInżynierka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OderFeedController : ControllerBase
    {
        private readonly IOrderFeedDatabaseService _dbService;

        public OderFeedController(IOrderFeedDatabaseService dbService)
        {
            _dbService = dbService;
        }

        [Route("AddOrderFeed")]
        [HttpPost]
        public async Task<IActionResult> AddOrderFeed(AddOrderFeedDTO orderHatchery, [FromQuery(Name = "farmId")] int farmId)
        {
            await _dbService.AddOrderFeed(orderHatchery, farmId);

            return Ok();
        }
    }
}
