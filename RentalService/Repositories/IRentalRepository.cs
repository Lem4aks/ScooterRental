using RentalService.Entities;
using RentalService.Models;

namespace RentalService.Repositories
{
    public interface IRentalRepository
    {
        Task<Session> GetSessionInfo(Guid id);

        Task<Guid> StartSession(Guid clientId, Guid scooterId);

        Task<Session> EndSession(Guid sessionId);
    }
}
