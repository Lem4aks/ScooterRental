using ClientService.Entities;
namespace ClientService.Repositories
{
    public interface IClientRepository
    {
        Task<ClientEntity> GetClientByEmail(string email);
        Task<ClientEntity> GetClientByUserName(string userName);
        Task<bool> AddClient(ClientEntity client);
    }
}
