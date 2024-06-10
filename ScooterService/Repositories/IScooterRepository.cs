using ScooterService.Entities;

namespace ScooterService.Repositories
{
    public interface IScooterRepository
    {
        Task<List<ScooterEntity>> GetAvailableScooters();
        Task AddScooterAsync(ScooterEntity scooter);

        Task<bool> DeleteScooterAsync(Guid id);

        Task<bool> UpdateScooterStatusAsync(Guid id, bool status);

        Task<List<ScooterEntity>> GetAllScooters();
    }
}
