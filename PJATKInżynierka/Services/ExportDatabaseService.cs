using Microsoft.EntityFrameworkCore;
using PJATKInżynierka.DTOs.ExportDTOs;
using PJATKInżynierka.Models;

namespace PJATKInżynierka.Services
{
    public class ExportDatabaseService :IExportDatabaseService
    {
        private readonly pjatkContext _pjatkContext;

        public ExportDatabaseService()
        {
            _pjatkContext = new pjatkContext();
        }

        public async Task AddExport(AddExportDTO export, int cycleId)
        {
            var dateDelivery = await _pjatkContext.DateDeliveries.Where(x => x.DateDelivery1 == export.Date).FirstOrDefaultAsync();

            if (dateDelivery == null)
            {
                await _pjatkContext.DateDeliveries.AddAsync(new DateDelivery
                {
                    DateDelivery1 = export.Date,
                    WorkingDate = true,
                    SlaughterhouseId = 1
                });
            }
            else if (dateDelivery.WorkingDate == false)
            {
                throw new Exception("Slaughterhouse does not work on that day");
            }

            await _pjatkContext.Exports.AddAsync(new Export
            {
                Date = export.Date,
                NumberMale = export.NumberMale,
                NumberFemale = export.NumberFemale,
                Weight = export.Weight,
                Price = export.Price,
                CycleId = cycleId
            });

            await _pjatkContext.SaveChangesAsync();

            var exportId =_pjatkContext.Exports.OrderBy( x=> x.ExportId).LastAsync().Result.ExportId;

            await _pjatkContext.Deliveries.AddAsync(new Delivery
            {
                ExportId = exportId,
                DateDelivery = export.Date
            }) ;

            await _pjatkContext.SaveChangesAsync();
        }

        public async Task<List<Export>> GetExports(int cycleId)
        {
            var exports = await _pjatkContext.Exports.Where(x => x.CycleId == cycleId).ToListAsync();

            return exports;
        }
    }
}
