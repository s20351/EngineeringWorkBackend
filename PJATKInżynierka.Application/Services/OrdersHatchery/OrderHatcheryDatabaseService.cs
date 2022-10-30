using Domain.Models;
using Microsoft.EntityFrameworkCore;
using PJATKInżynierka.DTOs.OrderHatcheryDTOs;

namespace Application.Services.OrdersHatchery
{
    public class OrderHatcheryDatabaseService : IOrderHatcheryDatabaseService
    {
        private readonly pjatkContext _pjatkContext;

        public OrderHatcheryDatabaseService()
        {
            _pjatkContext = new pjatkContext();
        }

        public async Task AddOrderHatchery(AddOrderHatcheryDTO orderHatchery, int farmId)
        {
            await _pjatkContext.OrderHatcheries.AddAsync(new OrderHatchery
            {
                HatcheryHatcheryId = orderHatchery.HatcheryID,
                DataOfOrder = orderHatchery.DateOfOrder,
                DataOfArrival = orderHatchery.DateOfArrival,
                NumberMale = orderHatchery.NumberMale,
                NumberFemale = orderHatchery.NumberFemale,
                FarmFarmId = farmId
            });

            await _pjatkContext.SaveChangesAsync();
        }

        public async Task<List<DateTime>> GetDeliveriesDates(int farmId)
        {
            var deliveryDates = new List<DateTime>();
            var ordersHatchery = await _pjatkContext.OrderHatcheries.Where(x => x.FarmFarmId == farmId).ToListAsync();

            foreach (var orderHatchery in ordersHatchery)
            {
                deliveryDates.Add(orderHatchery.DataOfArrival);
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