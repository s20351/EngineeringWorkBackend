using Microsoft.AspNetCore.Mvc;
using PJATKInżynierka.DTOs.FarmsDTOs;
using PJATKInżynierka.Services;

namespace PJATKInżynierka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmsController : ControllerBase
    {
        private readonly IFarmsDatebaseService _dbService;

        public FarmsController(IFarmsDatebaseService dbService)
        {
            _dbService = dbService;
        }

        [Route("{farmerID}")]
        [HttpGet]
        public async Task<IActionResult> GetFarms(int farmerID)
        {
            var farms = await _dbService.GetFarms(farmerID);

            return Ok(farms);
        }

        [Route("AddFarm/{farmerId}")]
        [HttpPost]
        public async Task<IActionResult> AddFarmer(AddFarmDTO farm, int farmerId)
        {
            await _dbService.AddFarm(farm, farmerId);

            return StatusCode(200, "Farm added");
        }

        [Route("GetObjectCurrentInfo/{farmID}")]
        [HttpGet]
        public async Task<IActionResult> GetObjectCurrentInfo(int farmId)
        {
            var objectInfo = await _dbService.GetObjectCurrentInfo(farmId);

            return Ok(objectInfo);
        }

        [Route("DeleteFarm/{farmId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteFarm(int farmId)
        {
            await _dbService.DeleteFarm(farmId);

            return StatusCode(200, "Farm deleted");
        }
    }
}
