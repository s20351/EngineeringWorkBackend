using Microsoft.EntityFrameworkCore;
using PJATKInżynierka.DTOs.CyclesDTOs;
using Domain.Models;

namespace Application.Services.Cycles
{
    public class CycleDatabaseService : ICycleDatabaseService
    {
        private readonly pjatkContext _pjatkContext;

        public CycleDatabaseService()
        {
            _pjatkContext = new pjatkContext();
        }

        public async Task CreateCycle(CreateCycleDTO cycle, int farmId)
        {
            await _pjatkContext.Cycles.AddAsync(new Cycle
            {
                Description = cycle.Description,
                DateIn = cycle.DateIn,
                DateOut = cycle.DateOut,
                NumberMale = cycle.NumberMale,
                NumberFemale = cycle.NumberFemale,
                FarmFarmId = farmId
            });

            await _pjatkContext.SaveChangesAsync();
        }

        public async Task<List<Cycle>> GetCycles(int farmId)
        {
            var cycles = await _pjatkContext.Cycles.Where(x => x.FarmFarmId == farmId).ToListAsync();

            return cycles;
        }
    }
}
