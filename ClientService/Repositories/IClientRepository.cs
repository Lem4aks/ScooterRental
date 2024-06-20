
using ClientService.Entities;
using ClientService.Models;
namespace ClientService.Repositories
{
    public interface IClientRepository
    {
        Task<Client> GetClientByEmail(string email);
        Task<Client> GetClientByUserName(string userName);
        
        Task<bool> AddClient(Client client);

        Task<bool> AddSession(Guid clientId, Guid sessionId);
    }
}
