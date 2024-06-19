namespace APIGateway.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<bool> RegisterClient(string username, string password, string email);
    }
}