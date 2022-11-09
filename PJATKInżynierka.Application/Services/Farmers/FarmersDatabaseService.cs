using Domain.DTOs.FarmersDTOs;
using Domain.DTOs.FarmsDTOs;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using PJATKInżynierka.DTOs.FarmersDTOs;
using PJATKInżynierka.DTOs.FarmsDTOs;
using System.Globalization;

namespace Application.Services.Farmers
{
    public class FarmersDatabaseService : IFarmersDatabaseService
    {
        private readonly pjatkContext _pjatkContext;

        public FarmersDatabaseService(pjatkContext pjatkContext)
        {
            _pjatkContext = pjatkContext;
        }

        public async Task AddFarmer(AddFarmerDTO farmer)
        {
            await _pjatkContext.Farmers.AddAsync(new Farmer
            {
                Name = farmer.Name,
                Surname = farmer.Surname,
                FarmerColor = farmer.FarmerColor,
            });

            await _pjatkContext.SaveChangesAsync();
        }

        public async Task<List<FarmEventDTO>> GetFarmerEvents(int farmerId)
        {
            List<FarmEventDTO> list = new List<FarmEventDTO>();
            await _pjatkContext.Farmers.LoadAsync();
            var farms = await _pjatkContext.Farms.Where(x => x.FarmerFarmerId == farmerId).ToListAsync();
            foreach(var farm in farms)
            {
                var cycles = await _pjatkContext.Cycles.Where(x => x.FarmFarmId == farm.FarmId).ToListAsync();
                foreach(var cycle in cycles)
                {
                    list.Add(new FarmEventDTO
                    {
                        Title = $"Wstawienie {farm.FarmerFarmer.Surname}",
                        AllDay = true,
                        Start = cycle.DateIn.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                        End = cycle.DateIn.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                    });

                    list.Add(new FarmEventDTO
                    {
                        Title = $"Koniec rzutu {farm.FarmerFarmer.Surname}",
                        AllDay = true,
                        Start = cycle.DateOut.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                        End = cycle.DateOut.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                    });

                    var exports = await _pjatkContext.Exports.Where(x => x.CycleCycleId == cycle.CycleId).ToListAsync();
                    await _pjatkContext.Terms.LoadAsync();
                    foreach(var export in exports)
                    {
                        list.Add(new FarmEventDTO
                        {
                            Title = $"Zdawanie {farm.FarmerFarmer.Surname}",
                            AllDay = true,
                            Start = export.TermTerm.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                            End = export.TermTerm.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                        });
                    }
                }
            }
            
            return list;
        }

        public async Task<List<GetFarmerDTO>> GetFarmers()
        {
            var list = new List<GetFarmerDTO>();

            var farmers = await _pjatkContext.Farmers.ToListAsync();
            foreach(var farmer in farmers)
            {
                list.Add(new GetFarmerDTO
                {
                    FarmerId = farmer.FarmerId,
                    FarmerName = $"{farmer.Name} {farmer.Surname}"
                });
            }

            return list;
        }

        public async Task<Farmer> GetFarmers(int farmerID)
        {
            var farmer = await _pjatkContext.Farmers.FirstOrDefaultAsync(x => x.FarmerId == farmerID);

            if (farmer != null)
            {
                return farmer;
            }
            else
            {
                throw new Exception($"Farmer with ID:{farmerID} doesn't exist");
            }
        }

    }
}
