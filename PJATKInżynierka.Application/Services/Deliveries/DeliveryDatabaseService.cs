using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.DTOs.DeliveriesDTOs;
using Infrastructure.Database;

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

            var termID = _pjatkContext.Terms.OrderBy(x => x.TermId).LastAsync().Result.TermId;

            await _pjatkContext.Deliveries.AddAsync(new Delivery
            {
                TermTermId = termID,
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
            
            foreach (var delivery in deliveries)
            {
                if(delivery.TermTerm.Exports.Any())
                {
                    foreach(var export in delivery.TermTerm.Exports)
                    {
                        getDeliveriesDTOs.Add(new GetDeliveriesDTO
                        {
                            Date = delivery.TermTerm.Date,
                            Name = export.CycleCycle.FarmFarm.FarmerFarmer.Name,
                            Surname = export.CycleCycle.FarmFarm.FarmerFarmer.Surname,
                            Weight = delivery.Weight
                        });
                    }
                }
                else
                {
                    getDeliveriesDTOs.Add(new GetDeliveriesDTO
                    {
                        Date = delivery.TermTerm.Date,
                        Weight = delivery.Weight
                    });
                } 

            }

            return getDeliveriesDTOs;
        }
    }
}
