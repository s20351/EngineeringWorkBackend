using PJATKInżynierka.DTOs.CyclesDTOs;
using PJATKInżynierka.Models;

namespace PJATKInżynierka.Services
{
    public interface ICycleDatabaseService
    {
        public Task CreateCycle(CreateCycleDTO cycle, int farmId);
        public Task<List<Cycle>> GetCycles(int farmId);
    }
}
