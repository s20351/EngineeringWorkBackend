using PJATKInżynierka.DTOs.OrderFeedDTOs;
using Domain.Models;
using Domain.DTOs.OrderFeedDTOs;
using Domain.DTOs.FarmsDTOs;

namespace Application.Services.OrdersFeed
{
    public interface IOrderFeedDatabaseService
    {
        public Task AddOrderFeed(AddOrderFeedDTO orderFeed, int farmId);
        public Task<List<OrderFeed>> GetOrdersFeed(int farmId);
        public Task<List<DateTime>> GetDeliveriesDates(int farmId);
        public Task<List<GetOrdersScheduleDTO>> GetOrdersSchedule(int farmerId);
        public Task<List<FarmEventDTO>> GetFeedFarmerEvents(int farmerId);
    }
}
