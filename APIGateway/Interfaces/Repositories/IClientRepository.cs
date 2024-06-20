namespace APIGateway.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<bool> RegisterClient(string username, string password, string email);

        Task<string> Login(string email, string password);
    }
}