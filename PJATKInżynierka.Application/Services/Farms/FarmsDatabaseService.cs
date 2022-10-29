using Microsoft.EntityFrameworkCore;
using PJATKInżynierka.DTOs.FarmsDTOs;
using PJATKInżynierka.Models;

namespace Application.Services.Farms
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

        public async Task<GetObjectInfoDTO> GetObjectCurrentInfo(int farmId)
        {
            var farm = await _pjatkContext.Farms.Where(x => x.FarmId == farmId).FirstOrDefaultAsync();
            var cycle = await _pjatkContext.Cycles.Where(x => x.FarmId == farmId && x.DateIn <= DateTime.Now && (x.DateOut > DateTime.Now || x.DateOut == null)).FirstOrDefaultAsync();
            
            if(cycle == null)
            {
                return null;
            }

            var orderHatchery = await _pjatkContext.OrderHatcheries.Where(x => x.FarmId == farmId && x.DateOfArrival == cycle.DateIn).FirstOrDefaultAsync();

            var export = await _pjatkContext.Exports.Where(x => x.CycleId == cycle.CycleId).ToListAsync();

            var objectInfo = new GetObjectInfoDTO
            {
                ObjectID = farmId,
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
        private int CalculateDaysToExport(List<Export> export)
        {
            if (export.Any())
            {
                var daysToExport = (int)(export.OrderByDescending(x => x.Date).Last().Date - DateTime.Now).TotalDays;
                return daysToExport;
            }
            else
            {
                return -1;
            }
        }

        public async Task DeleteFarm(int farmId)
        {
            var farm = await _pjatkContext.Farms.FirstAsync(x => x.FarmId == farmId);
            _pjatkContext.Farms.Remove(farm);

            await _pjatkContext.SaveChangesAsync();
        }

        public async Task<List<GetObjectInfoDTO>> GetHome(int farmerID)
        {
            var farms = await _pjatkContext.Farms.Where(x => x.FarmerId == farmerID).ToListAsync();

            List<GetObjectInfoDTO> list = new List<GetObjectInfoDTO>();

            foreach (Farm farm in farms)
            {
                list.Add(GetObjectCurrentInfo(farm.FarmId).Result);
            }

            return list;
        }
    }
}
