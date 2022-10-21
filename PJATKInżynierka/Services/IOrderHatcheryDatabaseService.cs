using PJATKInżynierka.DTOs.OrderHatcheryDTOs;
using PJATKInżynierka.Models;

namespace PJATKInżynierka.Services
{
    public interface IOrderHatcheryDatabaseService
    {
        public Task AddOrderHatchery(AddOrderHatcheryDTO orderHatchery, int farmId);
        public Task<List<OrderHatchery>> GetOrdersHatchery(int farmId);
    }
}
