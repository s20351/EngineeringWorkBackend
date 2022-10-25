using Microsoft.EntityFrameworkCore;
using PJATKInżynierka.DTOs.OrderFeedDTOs;
using PJATKInżynierka.Models;

namespace Application.Services.OrdersFeed
{
    public class OrderFeedDatabaseService : IOrderFeedDatabaseService
    {
        private readonly pjatkContext _pjatkContext;

        public OrderFeedDatabaseService()
        {
            _pjatkContext = new pjatkContext();
        }

        public async Task AddOrderFeed(AddOrderFeedDTO orderFeed, int farmId)
        {
            await _pjatkContext.OrderFeeds.AddAsync(new OrderFeed
            {
                SupplierName = orderFeed.SupplierName,
                DateOfArrival = orderFeed.DateOfArrival,
                DateOfOrder = orderFeed.DateOfOrder,
                Weight = orderFeed.Weight,
                Price = orderFeed.Price,
                FarmId = farmId
            });

            await _pjatkContext.SaveChangesAsync();
        }

        public async Task<List<OrderFeed>> GetOrdersFeed(int farmId)
        {
            var ordersFeed = await _pjatkContext.OrderFeeds.Where(x => x.FarmId == farmId).ToListAsync();
            return ordersFeed;
        }
    }
}
