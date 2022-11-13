using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using PJATKInżynierka.DTOs.ExportDTOs;

namespace Application.Services.Exports
{
    public class ExportDatabaseService : IExportDatabaseService
    {
        private readonly pjatkContext _pjatkContext;

        public ExportDatabaseService(pjatkContext pjatkContext)
        {
            _pjatkContext = pjatkContext;
        }

        public async Task AddExport(AddExportDTO export, int cycleId)
        {
            var dateDelivery = await _pjatkContext.Terms.Where(x => x.Date == export.Date).FirstOrDefaultAsync();

            if (dateDelivery == null)
            {
                await _pjatkContext.Terms.AddAsync(new Term
                {
                    Date = export.Date,
                    IsWorkingDay = true,
                });
            }
            else if (dateDelivery.IsWorkingDay == false)
            {
                throw new Exception("Slaughterhouse does not work on that day");
            }

            await _pjatkContext.SaveChangesAsync();

            var term = await _pjatkContext.Terms.FirstAsync(x => x.Date == export.Date);

            await _pjatkContext.Exports.AddAsync(new Export
            {
                TermTermId = term.TermId,
                NumberMale = export.NumberMale,
                NumberFemale = export.NumberFemale,
                Weight = export.Weight,
                CycleCycleId = cycleId
            });

            await _pjatkContext.Deliveries.AddAsync(new Delivery
            {
                Weight = export.Weight,
                TermTermId = term.TermId,
            });

            await _pjatkContext.SaveChangesAsync();
        }

        public async Task<List<Export>> GetExports(int cycleId)
        {
            var exports = await _pjatkContext.Exports.Where(x => x.CycleCycleId == cycleId).ToListAsync();

            return exports;
        }
    }
}
