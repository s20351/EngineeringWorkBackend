using Microsoft.EntityFrameworkCore;
using PJATKInżynierka.DTOs.FarmsDTOs;
using PJATKInżynierka.Models;

namespace PJATKInżynierka.Services
{
    public class FarmsDatabaseService : IFarmsDatebaseService
    {
        private readonly pjatkContext _pjatkContext;

        public FarmsDatabaseService()
        {
            _pjatkContext = new pjatkContext();
        }

        public async Task AddFarm(AddFarmDTO farm, int farmerId)
        {
            await _pjatkContext.Farms.AddAsync(new Farm
            {
                Name = farm.Name,
                Address = farm.Address,
                FarmColor = farm.FarmColor,
                FarmerId = farmerId
            });

            await _pjatkContext.SaveChangesAsync();
        }

        public async Task<List<Farm>> GetFarms(int farmerID)
        {
            var farms = await _pjatkContext.Farms.Where(x => x.FarmerId == farmerID).ToListAsync();

            return farms;
        }

        public async Task<GetObjectInfoDTO> GetObjectInfo(int farmId)
        {
            var farm = await _pjatkContext.Farms.Where(x => x.FarmId == farmId).FirstOrDefaultAsync();
            var cycle = await _pjatkContext.Cycles.Where(x => x.FarmId == farmId && (x.DateIn<DateTime.Now && x.DateOut>DateTime.Now)).FirstOrDefaultAsync();
            var orderHatchery = await _pjatkContext.OrderHatcheries.Where(x => x.FarmId == farmId && x.DateOfArrival == cycle.DateIn).FirstOrDefaultAsync();
            var export = await _pjatkContext.Exports.Where(x => x.CycleId == cycle.CycleId).OrderByDescending(x => x.Date).LastAsync();

            var objectInfo = new GetObjectInfoDTO
            {
                ObjectName = farm.Name,
                AliveMale = cycle.NumberMale,
                AliveFemale = cycle.NumberFemale,
                DeadMale = CalculateDeadMale(cycle, orderHatchery),
                DeadFemale = CalculateDeadFemale(cycle, orderHatchery),
                BreedingDay = (int)(DateTime.Now - cycle.DateIn).TotalDays,
                DaysToExport = CalculateDaysToExport(export)
            };
            return objectInfo;
        }

        private int CalculateDeadMale(Cycle cycle, OrderHatchery orderHatchery)
        {
            var deadMale = orderHatchery.NumberMale - cycle.NumberMale;
            return deadMale;
        }

        private int CalculateDeadFemale(Cycle cycle, OrderHatchery orderHatchery)
        {
            var deadMale = orderHatchery.NumberFemale - cycle.NumberFemale;
            return deadMale;
        }
        private int CalculateDaysToExport(Export export)
        {
            var daysToExport = (int)(export.Date - DateTime.Now).TotalDays;
            return daysToExport;
        }
    }
}
