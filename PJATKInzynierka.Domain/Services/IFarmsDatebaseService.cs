using PJATKInżynierka.DTOs.FarmsDTOs;
using PJATKInżynierka.Models;

namespace Application.Services.Farms
{
    public interface IFarmsDatebaseService
    {
        public Task<List<Farm>> GetFarms(int farmerID);
        public Task AddFarm(AddFarmDTO farm, int farmerId);
        public Task<GetObjectInfoDTO> GetObjectCurrentInfo(int farmId);
        public Task DeleteFarm(int farmId);
        public Task<List<GetObjectInfoDTO>> GetHome(int farmerID);
    }
}
