using PJATKInżynierka.DTOs.OrderHatcheryDTOs;
using PJATKInżynierka.Models;

namespace Application.Services.OrdersHatchery
{
    public interface IOrderHatcheryDatabaseService
    {
        public Task AddOrderHatchery(AddOrderHatcheryDTO orderHatchery, int farmId);
        public Task<List<OrderHatchery>> GetOrdersHatchery(int farmId);
    }
}
