using Domain.DTOs.CyclesDTOs;
using Domain.Models;
using PJATKInżynierka.DTOs.CyclesDTOs;

namespace Application.Services.Cycles
{
    public interface ICycleDatabaseService
    {
        public Task CreateCycle(CreateCycleDTO cycle, int farmId);
        public Task<List<GetCycleDTO>> GetCycles(int farmId);
    }
}
