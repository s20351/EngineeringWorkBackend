using Microsoft.EntityFrameworkCore;
using PJATKInżynierka.Models;

namespace Application.Services.DateDelivery
{
    public class DateDeliveryDatabaseService : IDateDeliveryDatabaseService
    {
        private readonly pjatkContext _pjatkContext;

        public DateDeliveryDatabaseService()
        {
            _pjatkContext = new pjatkContext();
        }

        public async Task<List<Delivery>> GetDeliveries(DateTime date)
        {
            var dateDeliveries = await _pjatkContext.Deliveries.Where(x => x.DateDelivery == date).ToListAsync();

            return dateDeliveries;
        }
    }
}
