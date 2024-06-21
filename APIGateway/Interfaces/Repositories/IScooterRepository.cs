using APIGateway.Models;

namespace APIGateway.Interfaces.Repositories
{
    public interface IScooterRepository
    {
        Task AddScooter(string model);
        Task AddSession(Guid scooterId, Guid sessionId);
        Task<List<Scooter>> GetAvailableScooters();
        Task<List<Scooter>> GetScooters();
        Task RemoveScooter(Guid id);

        Task<Guid> GetScooterBySession(Guid sessionId);

        Task<string> GetScooterModel(Guid scooterId);
        Task UpdateScooterStatus(Guid id);
    }
}