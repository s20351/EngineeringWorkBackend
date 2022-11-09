using Application.Services.Farmers;
using Microsoft.AspNetCore.Mvc;
using PJATKInżynierka.DTOs.FarmersDTOs;

namespace PJATKInżynierka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmersController : ControllerBase
    {
        private readonly IFarmersDatabaseService _dbService;

        public FarmersController(IFarmersDatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFarmers()
        {
            var farmers = await _dbService.GetFarmers();

            return Ok(farmers);
        }

        [Route("{farmerID}")]
        [HttpGet]
        public async Task<IActionResult> GetFarmer(int farmerID)
        {
            var farmers = await _dbService.GetFarmers(farmerID);

            return Ok(farmers);
        }

        [Route("AddFarmer")]
        [HttpPost]
        public async Task<IActionResult> AddFarmer(AddFarmerDTO farmer)
        {
            await _dbService.AddFarmer(farmer);

            return Ok();
        }

        [Route("GetFarmerEvents/{farmerId}")]
        [HttpGet]
        public async Task<IActionResult> GetFarmerEvents(int farmerId)
        {
            var objectInfo = await _dbService.GetFarmerEvents(farmerId);

            return Ok(objectInfo);
        }
    }
}
