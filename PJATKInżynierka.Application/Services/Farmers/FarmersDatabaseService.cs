using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using PJATKInżynierka.DTOs.FarmersDTOs;
using PJATKInżynierka.DTOs.FarmsDTOs;

namespace Application.Services.Farmers
{
    public class FarmersDatabaseService : IFarmersDatabaseService
    {
        private readonly pjatkContext _pjatkContext;

        public FarmersDatabaseService(pjatkContext pjatkContext)
        {
            _pjatkContext = pjatkContext;
        }

        public async Task AddFarmer(AddFarmerDTO farmer)
        {
            await _pjatkContext.Farmers.AddAsync(new Farmer
            {
                Name = farmer.Name,
                Surname = farmer.Surname,
                FarmerColor = farmer.FarmerColor,
            });

            await _pjatkContext.SaveChangesAsync();
        }

        public async Task<List<Farmer>> GetFarmers()
        {
            var farmers = await _pjatkContext.Farmers.ToListAsync();

            return farmers;
        }

        public async Task<Farmer> GetFarmers(int farmerID)
        {
            var farmer = await _pjatkContext.Farmers.FirstOrDefaultAsync(x => x.FarmerId == farmerID);

            if (farmer != null)
            {
                return farmer;
            }
            else
            {
                throw new Exception($"Farmer with ID:{farmerID} doesn't exist");
            }
        }

    }
}
