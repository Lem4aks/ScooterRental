using Aggregator.Models;

namespace Aggregator.Services
{
    public interface IRentalService
    {
        Task<Rental> GetSessionInfo(Guid id);

        Task<Rental> StartSession(Guid sessionId, Guid clientId, Guid scooterId);

        Task<Rental> EndSession(Guid sessionId);
    }
}
