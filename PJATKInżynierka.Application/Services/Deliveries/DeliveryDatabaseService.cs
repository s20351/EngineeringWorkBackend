using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.DTOs.DeliveriesDTOs;
using Infrastructure.Database;
using System.Globalization;
using Domain.DTOs.FarmsDTOs;

namespace Application.Services.DateDelivery
{
    public class DeliveryDatabaseService : IDeliveryDatabaseService
    {
        private readonly pjatkContext _pjatkContext;

        public DeliveryDatabaseService(pjatkContext pjatkContext)
        {
            _pjatkContext = pjatkContext;
        }

        public async Task AddDelivery(AddDeliveryDTO addDeliveryDTO)
        {
            var dateDelivery = await _pjatkContext.Terms.Where(x => x.Date == addDeliveryDTO.DeliveryDate).FirstOrDefaultAsync();

            if (dateDelivery == null)
            {
                await _pjatkContext.Terms.AddAsync(new Term
                {
                    Date = addDeliveryDTO.DeliveryDate,
                    IsWorkingDay = true,
                });
            }
            else if (dateDelivery.IsWorkingDay == false)
            {
                throw new Exception("Slaughterhouse does not work on that day");
            }

            await _pjatkContext.SaveChangesAsync();

            var term = await _pjatkContext.Terms.FirstAsync(x => x.Date == addDeliveryDTO.DeliveryDate);

            await _pjatkContext.Deliveries.AddAsync(new Delivery
            {
                TermTermId = term.TermId,
                Weight = addDeliveryDTO.Weight,
            });

            await _pjatkContext.SaveChangesAsync();
        }

        public async Task<List<Delivery>> GetDeliveries(DateTime date)
        {
            var dateDeliveries = await _pjatkContext.Deliveries.Where(x => x.TermTerm.Date == date).ToListAsync();

            return dateDeliveries;
        }

        public async Task<List<GetDeliveriesDTO>> GetDeliveries()
        {
            List<GetDeliveriesDTO> getDeliveriesDTOs = new List<GetDeliveriesDTO>();

            await _pjatkContext.Farms.LoadAsync();
            await _pjatkContext.Farmers.LoadAsync();
            await _pjatkContext.Cycles.LoadAsync();
            await _pjatkContext.Exports.LoadAsync();
            await _pjatkContext.Terms.LoadAsync();

            var deliveries = await _pjatkContext.Deliveries.Where(x => x.TermTerm.Date >= DateTime.Now).ToListAsync();
            var exports = await _pjatkContext.Exports.ToListAsync();

            List<int> termIds = new List<int>();

            foreach(var export in exports)
            {
                getDeliveriesDTOs.Add(new GetDeliveriesDTO
                {
                    Date = export.TermTerm.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Name = export.CycleCycle.FarmFarm.FarmerFarmer.Name,
                    Surname = export.CycleCycle.FarmFarm.FarmerFarmer.Surname,
                    Weight = export.Weight
                });

                termIds.Add((int)export.TermTermId!);
            }


            foreach (var delivery in deliveries)
            {
                if(!termIds.Contains(delivery.TermTermId))
                {
                    getDeliveriesDTOs.Add(new GetDeliveriesDTO
                    {
                        Date = delivery.TermTerm.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                        Weight = delivery.Weight,
                        Name = "Obcy hodowca",
                        Surname = "Obcy hodowca"
                    });
                }
                else
                {
                    termIds.Remove(delivery.TermTermId);
                }
            }
            
            return getDeliveriesDTOs.OrderBy(x => x.Date).ToList(); 
        }

        public async Task<List<FarmEventDTO>> GetEvents()
        {
            List<FarmEventDTO> events = new List<FarmEventDTO>();

            List<GetDeliveriesDTO> deliveries = await GetDeliveries();

            foreach (var delivery in deliveries)
            {
                if (!delivery.Name!.Equals("Obcy hodowca"))
                {
                        events.Add(new FarmEventDTO
                        {
                            Title = $"Dostawa od {delivery.Name} {delivery.Surname} ",
                            AllDay = true,
                            Start = delivery.Date,
                            End = delivery.Date
                        });
                }
                else
                {
                    events.Add(new FarmEventDTO
                    {
                        Title = $"Dostawa od obcego hodowcy",
                        AllDay = true,
                        Start = delivery.Date,
                        End = delivery.Date
                    });
                }
            }

            return events;
        }
    }
}
