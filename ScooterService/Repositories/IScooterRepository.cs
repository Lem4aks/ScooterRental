using ScooterService.Entities;
using ScooterService.Models;

namespace ScooterService.Repositories
{
    public interface IScooterRepository
    {
        Task<List<Scooter>> GetAvailableScooters();
        Task<bool> AddScooterAsync(Scooter scooter);

        Task<bool> DeleteScooterAsync(Guid id);

        Task<bool> UpdateScooterStatusAsync(Guid id);

        Task<List<Scooter>> GetAllScooters();

        Task<string> GetScooterModel(Guid id);

        Task<Guid> GetScooterIdBySession(Guid sessionId);

        Task<bool> AddSession(Guid sessionId, Guid scooterId);
    }
}
