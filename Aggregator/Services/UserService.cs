using Aggregator.Models;
using ClientAccount;
using Grpc.Net.Client;

namespace Aggregator.Services
{
    public class UserService : IUserService
    {
        private readonly ClientAccount.ClientService.ClientServiceClient _clientServiceClient;

        public UserService(ClientAccount.ClientService.ClientServiceClient clientServiceClient)
        {
            _clientServiceClient = clientServiceClient;
        }

        public async Task Login(User user)
        {
            var request = new AuthenticateClientRequest
            {
                Identifier = user.userName,
                Password = user.password
            };

            var response = await _clientServiceClient.AuthenticateClientAsync(request);

            if (!response.IsSuccess)
            {
                // Handle unsuccessful login attempt
                throw new Exception(response.ErrorMessage);
            }
        }

        public void Logout()
        {
            // Not implemented
        }

        public async Task<bool> Registration(User user)
        {
            var request = new RegisterClientRequest
            {
                Email = user.email,
                UserName = user.userName,
                Password = user.password
            };

            var response = await _clientServiceClient.RegisterClientAsync(request);

            if (!response.IsSuccess)
            {
                // Handle unsuccessful registration attempt
                throw new Exception(response.ErrorMessage);
            }

            return response.IsSuccess;
        }
    }
}
