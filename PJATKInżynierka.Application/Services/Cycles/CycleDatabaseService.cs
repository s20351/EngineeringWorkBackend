using Microsoft.EntityFrameworkCore;
using PJATKInżynierka.DTOs.CyclesDTOs;
using Domain.Models;
using Infrastructure.Database;
using Domain.DTOs.CyclesDTOs;
using System.Globalization;

namespace Application.Services.Cycles
{
    public class CycleDatabaseService : ICycleDatabaseService
    {
        private readonly pjatkContext _pjatkContext;

        public CycleDatabaseService(pjatkContext pjatkContext)
        {
            _pjatkContext = pjatkContext;
        }

        public async Task CreateCycle(CreateCycleDTO cycle, int farmId)
        {
            var hatcheryOrder = await _pjatkContext.OrderHatcheries.FirstOrDefaultAsync(x => x.OrderHatcheryId == cycle.HatcheryOrderID);

            await _pjatkContext.Cycles.AddAsync(new Cycle
            {
                Description = cycle.Description,
                DateIn = hatcheryOrder.DataOfArrival,
                DateOut = cycle.DateOut,
                NumberMale = hatcheryOrder.NumberMale,
                NumberFemale = hatcheryOrder.NumberFemale,
                FarmFarmId = farmId
            });

            await _pjatkContext.SaveChangesAsync();
        }

        public async Task<List<GetCycleDTO>> GetCycles(int farmerId)
        {
            List<GetCycleDTO> cycles = new List<GetCycleDTO>();

            var farms = await _pjatkContext.Farms.Where(x => x.FarmerFarmerId == farmerId).ToListAsync();
            if(farms.Any())
            {
                foreach(var farm in farms)
                {
                    var farmCycles = await _pjatkContext.Cycles.Where(x => x.FarmFarmId == farm.FarmId).ToListAsync();

                    foreach(var farmCycle in farmCycles)
                    {
                        cycles.Add(new GetCycleDTO
                        {
                            CycleId = farmCycle.CycleId,
                            CycleDescription = farmCycle.Description,
                            EndCycleDate = farmCycle.DateOut.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                        });
                    }
                }
            }

            return cycles;
        }
    }
}
