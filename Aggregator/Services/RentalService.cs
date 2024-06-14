using Aggregator.Models;
namespace Aggregator.Services
{
    public class RentalService : IRentalService
    {
        public Task<Rental> EndSession(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<Rental> GetSessionInfo(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Rental> StartSession(Guid sessionId, Guid clientId, Guid scooterId)
        {
            throw new NotImplementedException();
        }
    }
}
