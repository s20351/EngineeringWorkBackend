using Domain.DTOs.FarmersDTOs;
using Domain.DTOs.FarmsDTOs;
using Domain.Models;
using PJATKInżynierka.DTOs.FarmersDTOs;

namespace Application.Services.Farmers
{
    public interface IFarmersDatabaseService
    {
        public Task<List<GetFarmerDTO>> GetFarmers();
        public Task<Farmer> GetFarmers(int farmerID);
        public Task AddFarmer(AddFarmerDTO farmer);
        public Task<List<FarmEventDTO>> GetFarmerEvents(int farmerId);
    }
}
