using PJATKInżynierka.Models;

namespace Application.Services.DateDelivery
{
    public interface IDateDeliveryDatabaseService
    {
        public Task<List<Delivery>> GetDeliveries(DateTime date);
    }
}
