using Aggregator.Models;

namespace Aggregator.Services
{
    public interface IScooterService
    {
        public Task<List<Scooter>> GetScooterList();
        public Task AddScooter(Scooter scooter);
        public Task RemoveScooter(Guid Id);
    }
}
