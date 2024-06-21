using APIGateway.Models;

namespace APIGateway.Interfaces.Repositories
{
    public interface ISessionRepository
    {
        Task<SessionDto> EndSession(Guid sessionId);
        Task<Session> GetSessionInfo(Guid sessionId);
        Task<Guid> StartSession(Guid clientId, Guid scooterId);
    }
}