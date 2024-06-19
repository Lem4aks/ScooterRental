using ScooterService.Entities;

namespace ScooterService.Repositories
{
    public interface IScooterRepository
    {
        Task<List<ScooterEntity>> GetAvailableScooters();
        Task AddScooterAsync(ScooterEntity scooter);

        Task<bool> DeleteScooterAsync(Guid id);

        Task<bool> UpdateScooterStatusAsync(Guid id);

        Task<List<ScooterEntity>> GetAllScooters();

        Task<bool> AddSession(Guid sessionId, Guid scooterId);
    }
}
