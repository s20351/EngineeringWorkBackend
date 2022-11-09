using Domain.DTOs.DeliveriesDTOs;
using Domain.DTOs.FarmsDTOs;
using Domain.Models;

namespace Application.Services.DateDelivery
{
    public interface IDeliveryDatabaseService
    {
        public Task<List<Delivery>> GetDeliveries(DateTime date);
        public Task<List<GetDeliveriesDTO>> GetDeliveries();
        public Task AddDelivery(AddDeliveryDTO addDeliveryDTO);
        public Task<List<FarmEventDTO>> GetEvents();
    }
}
