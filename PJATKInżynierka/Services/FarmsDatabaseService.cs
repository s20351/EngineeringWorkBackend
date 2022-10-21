using Microsoft.EntityFrameworkCore;
using PJATKInżynierka.DTOs.FarmsDTOs;
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

        public async Task AddFarm(AddFarmDTO farm, int farmerId)
        {
            await _pjatkContext.Farms.AddAsync(new Farm
            {
                Name = farm.Name,
                Address = farm.Address,
                FarmColor = farm.FarmColor,
                FarmerId = farmerId
            });

            await _pjatkContext.SaveChangesAsync();
        }

        public async Task<List<Farm>> GetFarms(int farmerID)
        {
            var farms = await _pjatkContext.Farms.Where(x => x.FarmerId == farmerID).ToListAsync();

            return farms;
        }


    }
}
