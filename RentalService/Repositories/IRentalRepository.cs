using RentalService.Entities;

namespace RentalService.Repositories
{
    public interface IRentalRepository
    {
        Task<SessionEntity> GetSessionInfo(Guid id);

        Task<SessionEntity> StartSession(Guid sessionId, Guid clientId, Guid scooterId);

        Task<SessionEntity> EndSession(Guid sessionId);
    }
}
