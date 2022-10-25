using Microsoft.EntityFrameworkCore;
using PJATKInżynierka.DTOs.OrderHatcheryDTOs;
using PJATKInżynierka.Models;

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
                SupplierName = orderHatchery.SupplierName,
                DateOfOrder = orderHatchery.DateOfOrder,
                DateOfArrival = orderHatchery.DateOfArrival,
                Price = orderHatchery.Price,
                NumberMale = orderHatchery.NumberMale,
                NumberFemale = orderHatchery.NumberFemale,
                FarmId = farmId
            });

            await _pjatkContext.SaveChangesAsync();
        }

        public async Task<List<OrderHatchery>> GetOrdersHatchery(int farmId)
        {
            var ordersHatchery = await _pjatkContext.OrderHatcheries.Where(x => x.FarmId == farmId).ToListAsync();
            return ordersHatchery;
        }
    }
}
