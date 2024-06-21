using APIGateway.Models;

namespace APIGateway.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<bool> RegisterClient(string username, string password, string email);

        Task AddSession(Guid sessionId, Guid clientId);
        Task<ClientDto> GetPersonalCabinet(Guid id);
        Task<string> Login(string email, string password);
    }
}