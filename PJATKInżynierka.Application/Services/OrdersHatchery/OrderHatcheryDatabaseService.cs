using Domain.DTOs.DeliveriesDTOs;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using PJATKInżynierka.DTOs.OrderHatcheryDTOs;

namespace Application.Services.OrdersHatchery
{
    public class OrderHatcheryDatabaseService : IOrderHatcheryDatabaseService
    {
        private readonly pjatkContext _pjatkContext;

        public OrderHatcheryDatabaseService(pjatkContext pjatkContext)
        {
            _pjatkContext = pjatkContext;
        }

        public async Task AddOrderHatchery(AddOrderHatcheryDTO orderHatchery, int farmId)
        {
            await _pjatkContext.OrderHatcheries.AddAsync(new OrderHatchery
            {
                HatcheryHatcheryId = orderHatchery.HatcheryID,
                DataOfOrder = DateTime.Now,
                DataOfArrival = orderHatchery.DateOfArrival,
                NumberMale = orderHatchery.NumberMale,
                NumberFemale = orderHatchery.NumberFemale,
                FarmFarmId = farmId
            });

            await _pjatkContext.SaveChangesAsync();
        }

        public async Task<List<GetDeliveriesDatesDTO>> GetDeliveriesDates(int farmId)
        {
            var deliveryDates = new List<GetDeliveriesDatesDTO>();
            var ordersHatchery = await _pjatkContext.OrderHatcheries.Where(x => x.FarmFarmId == farmId).ToListAsync();

            foreach (var orderHatchery in ordersHatchery)
            {
                deliveryDates.Add(new GetDeliveriesDatesDTO
                {
                    DeliveryID = orderHatchery.OrderHatcheryId,
                    Date = orderHatchery.DataOfArrival.ToShortDateString()
                });
            }

            return deliveryDates;
        }

        public async Task<List<OrderHatchery>> GetOrdersHatchery(int farmId)
        {
            var ordersHatchery = await _pjatkContext.OrderHatcheries.Where(x => x.FarmFarmId == farmId).ToListAsync();

            return ordersHatchery;
        }
    }
}