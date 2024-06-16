using Aggregator.Models;
using System.Threading.Tasks;

namespace Aggregator.Services
{
    public interface IUserService
    {
        Task Login(User user);
        void Logout();
        Task<bool> Registration(User user);
    }
}
