using APIGateway.Models;

namespace APIGateway.Interfaces.Repositories
{
    public interface ISessionRepository
    {
        Task<bool> EndSession(Guid sessionId);
        Task<Session> GetSessionInfo(Guid sessionId);
        Task<bool> StartSession(Guid clientId, Guid scooterId);
    }
}