using PJATKInżynierka.Models;

namespace PJATKInżynierka.Services
{
    public interface IDateDeliveryDatabaseService
    {
        public Task<List<Delivery>> GetDeliveries(DateTime date);
    }
}
