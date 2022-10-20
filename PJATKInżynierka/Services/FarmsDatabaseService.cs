using Microsoft.EntityFrameworkCore;
using PJATKInżynierka.Models;

namespace PJATKInżynierka.Services
{
    public class FarmsDatabaseService : IFarmsDatebaseService
    {
        private readonly pjatkContext _pjatkContext;

        public FarmsDatabaseService()
        {
            _pjatkContext = new pjatkContext();
        }

        public async Task<List<Farm>> GetFarms(int farmerID)
        {
            //var farms = await _pjatkContext.Farms.Where(x => x.FarmerId == farmerID).ToListAsync();

            var farms = await _pjatkContext.Farms.ToListAsync();
            return farms;
        }
    }
}
