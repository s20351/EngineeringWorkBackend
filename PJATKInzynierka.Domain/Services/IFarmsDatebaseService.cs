using Domain.DTOs.FarmsDTOs;
using PJATKInżynierka.DTOs.FarmsDTOs;

namespace Application.Services.Farms
{
    public interface IFarmsDatebaseService
    {
        public Task<List<GetFarmDTO>> GetFarms(int farmerId);
        public Task AddFarm(AddFarmDTO farm, int farmerId);
        public Task<GetObjectInfoDTO> GetObjectCurrentInfo(int farmId);
        public Task DeleteFarm(int farmId);
        public Task<List<GetObjectInfoDTO>> GetHomeDetails(int farmerId);
        public Task<List<FarmEventDTO>> GetAllFarmEvents(int farmId);
        public Task AddDeaths(AddDeathsDTO addDeaths, int farmId);
    }
}
