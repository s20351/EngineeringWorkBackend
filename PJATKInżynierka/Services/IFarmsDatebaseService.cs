using PJATKInżynierka.Models;

namespace PJATKInżynierka.Services
{
    public interface IFarmsDatebaseService
    {
        public Task<List<Farm>> GetFarms(int farmerID);
    }
}
