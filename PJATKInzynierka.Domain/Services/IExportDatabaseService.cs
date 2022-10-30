using Domain.Models;
using PJATKInżynierka.DTOs.ExportDTOs;


namespace Application.Services.Exports
{
    public interface IExportDatabaseService
    {
        public Task AddExport(AddExportDTO export, int cycleId);
        public Task<List<Export>> GetExports(int cycleId);
    }
}
