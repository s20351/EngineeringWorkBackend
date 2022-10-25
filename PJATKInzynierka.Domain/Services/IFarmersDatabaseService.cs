using PJATKInżynierka.DTOs.FarmersDTOs;
using PJATKInżynierka.Models;

namespace Application.Services.Farmers
{
    public interface IFarmersDatabaseService
    {
        public Task<List<Farmer>> GetFarmers();
        public Task<Farmer> GetFarmers(int farmerID);
        public Task AddFarmer(AddFarmerDTO farmer);
    }
}
