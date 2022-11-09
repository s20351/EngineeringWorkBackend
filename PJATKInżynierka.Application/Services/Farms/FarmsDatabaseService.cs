using Domain.DTOs.FarmsDTOs;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using PJATKInżynierka.DTOs.FarmsDTOs;
using System.Globalization;

namespace Application.Services.Farms
{
    public class FarmsDatabaseService : IFarmsDatebaseService
    {
        private readonly pjatkContext _pjatkContext;

        public FarmsDatabaseService(pjatkContext pjatkContext)
        {
            _pjatkContext = pjatkContext;
        }

        public async Task AddFarm(AddFarmDTO farm, int farmerId)
        {
            await _pjatkContext.Farms.AddAsync(new Farm
            {
                Name = farm.Name,
                AddressAddressId = farm.AddressID,
                FarmColor = farm.FarmColor,
                FarmerFarmerId = farmerId
            });

            await _pjatkContext.SaveChangesAsync();
        }

        public async Task<List<GetFarmDTO>> GetFarms(int farmerID)
        {
            var farms = await _pjatkContext.Farms.Where(x => x.FarmerFarmerId == farmerID).ToListAsync();

            List<GetFarmDTO> getFarmDTOs = new List<GetFarmDTO>();

            foreach(Farm farm in farms)
            {
                getFarmDTOs.Add(
                    new GetFarmDTO
                    {
                        FarmId = farm.FarmId,
                        Name = farm.Name
                    });
            }

            return getFarmDTOs;
        }

        public async Task<GetObjectInfoDTO> GetObjectCurrentInfo(int farmId)
        {
            var farm = await _pjatkContext.Farms.Where(x => x.FarmId == farmId).FirstOrDefaultAsync();
            var cycle = await _pjatkContext.Cycles.Where(x => x.FarmFarmId == farmId && x.DateIn <= DateTime.Now && (x.DateOut > DateTime.Now || x.DateOut == null)).FirstOrDefaultAsync();
            
            if(cycle == null)
            {
                return new GetObjectInfoDTO
                {
                    ObjectID = farmId,
                    ObjectName = farm.Name,
                    AliveMale = 0,
                    AliveFemale = 0,
                    DeadMale = 0,
                    DeadFemale = 0,
                    BreedingDay = 0,
                    DaysToExport = 0
                };
            }

            var orderHatchery = await _pjatkContext.OrderHatcheries.Where(x => x.FarmFarmId == farmId && x.DataOfArrival == cycle.DateIn).FirstOrDefaultAsync();

            var export = await _pjatkContext.Exports.Where(x => x.CycleCycleId == cycle.CycleId).ToListAsync();

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
            return (int)deadMale;
        }

        private int CalculateDeadFemale(Cycle cycle, OrderHatchery orderHatchery)
        {
            var deadFemale = orderHatchery.NumberFemale - cycle.NumberFemale;
            return (int)deadFemale;
        }
        private int CalculateDaysToExport(List<Export> exports)
        {
            if (exports.Any())
            {
                List<Term> exportDays = new List<Term>();

                foreach(Export export in exports)
                {
                    var exportDay = _pjatkContext.Terms.FirstOrDefault(x => x.TermId == export.TermTermId && x.Date > DateTime.Now);

                    if(exportDay != null)
                    exportDays.Add(exportDay);
                }

                var closestExportDay = exportDays.OrderByDescending(x => x.Date).Last().Date;

                var daysToExport = (int)(closestExportDay - DateTime.Now).TotalDays;

                return daysToExport;
            }
            else
            {
                return -1;
            }
        }

        public async Task DeleteFarm(int farmId)
        {
            var cycles = await _pjatkContext.Cycles.Where(x => x.FarmFarmId == farmId).ToListAsync();

            if(cycles.Any())
            {
                foreach(var cycle in cycles)
                { 
                    {
                        var exports = await _pjatkContext.Exports.Where(x => x.CycleCycleId == cycle.CycleId).ToListAsync();

                        if (exports.Any())
                        {
                            foreach(var export in exports)
                            {
                                _pjatkContext.Exports.Remove(export);
                            }
                        }

                        _pjatkContext.Cycles.Remove(cycle);
                    }
                }
            }
            var orderHatcheries = await _pjatkContext.OrderHatcheries.Where(x => x.FarmFarmId == farmId).ToListAsync();
            if(orderHatcheries.Any())
            {
                foreach (var order in orderHatcheries)
                {
                    _pjatkContext.OrderHatcheries.Remove(order);
                }
            }

            var orderFeeds = await _pjatkContext.OrderFeeds.Where(x => x.FarmFarmId == farmId).ToListAsync();
            if (orderFeeds.Any())
            {
                foreach (var order in orderFeeds)
                {
                    _pjatkContext.OrderFeeds.Remove(order);
                }
            }

            var farm = await _pjatkContext.Farms.FirstAsync(x => x.FarmId == farmId);
            _pjatkContext.Farms.Remove(farm);

            await _pjatkContext.SaveChangesAsync();
        }

        public async Task<List<GetObjectInfoDTO>> GetHomeDetails(int farmerID)
        {
            var farms = await _pjatkContext.Farms.Where(x => x.FarmerFarmerId == farmerID).ToListAsync();

            List<GetObjectInfoDTO> list = new List<GetObjectInfoDTO>();

            foreach (Farm farm in farms)
            {
                list.Add(GetObjectCurrentInfo(farm.FarmId).Result);
            }

            return list;
        }

        public async Task<List<FarmEventDTO>> GetAllFarmEvents(int farmId)
        {
            List<FarmEventDTO> list = new List<FarmEventDTO>();

            var orderFeeds = await _pjatkContext.OrderFeeds.Where(x => x.FarmFarmId == farmId).ToListAsync();
            var orderHatcheries = await _pjatkContext.OrderHatcheries.Where(x => x.FarmFarmId == farmId).ToListAsync();
            var cycles = await _pjatkContext.Cycles.Where(x => x.FarmFarmId == farmId).ToListAsync();
            await _pjatkContext.Terms.LoadAsync();

            foreach (var orderFeed in orderFeeds)
            {
                list.Add(new FarmEventDTO
                {
                    Title = "Pasza",
                    AllDay = true,
                    Start = orderFeed.DateOfArrival.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    End = orderFeed.DateOfArrival.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                });
            }

            foreach (var orderHatchery in orderHatcheries)
            {
                list.Add(new FarmEventDTO
                {
                    Title = "Pisklaki",
                    AllDay = true,
                    Start = orderHatchery.DataOfArrival.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    End = orderHatchery.DataOfArrival.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                });
            }

            foreach (var cycle in cycles)
            {
                list.Add(new FarmEventDTO
                {
                    Title = cycle.Description,
                    AllDay = true,
                    Start = cycle.DateIn.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    End = cycle.DateOut.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                });

                var exports = await _pjatkContext.Exports.Where(x => x.CycleCycleId == cycle.CycleId).ToListAsync();

                foreach(var export in exports)
                {
                    if(export.TermTerm != null)
                    {
                        list.Add(new FarmEventDTO
                        {
                            Title = "Zdawanie",
                            AllDay = true,
                            Start = export.TermTerm.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                            End = export.TermTerm.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                        });
                    }
                }
            }

            return list;
        }
    }
}
