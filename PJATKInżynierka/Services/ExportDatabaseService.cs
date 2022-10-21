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
        }

        public Task<List<Export>> GetExports(int cycleId)
        {
            var exports = _pjatkContext.Exports.Where(x => x.CycleId == cycleId).ToListAsync();

            return exports;
        }
    }
}
