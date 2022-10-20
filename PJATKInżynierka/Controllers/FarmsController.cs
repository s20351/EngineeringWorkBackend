using Microsoft.AspNetCore.Mvc;
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

        [Route("{farmID}")]
        [HttpGet]
        public async Task<IActionResult> GetFarms(int farmID)
        {
            var farms = await _dbService.GetFarms(farmID);

            return Ok(farms);
        }
    }
}
