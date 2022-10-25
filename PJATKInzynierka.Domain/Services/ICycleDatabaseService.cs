using PJATKInżynierka.DTOs.CyclesDTOs;
using PJATKInżynierka.Models;

namespace Application.Services.Cycles
{
    public interface ICycleDatabaseService
    {
        public Task CreateCycle(CreateCycleDTO cycle, int farmId);
        public Task<List<Cycle>> GetCycles(int farmId);
    }
}
