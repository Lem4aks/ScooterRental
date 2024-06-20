using ClientService.Models;

namespace ClientService.JWT
{
    public interface IJwtProvider
    {
        string GenerateToken(Client client);
    }
}