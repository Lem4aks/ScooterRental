using static ClientAccount.ClientService;
using AutoMapper;
using ClientAccount;
using APIGateway.Interfaces.Repositories;


namespace APIGateway.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientServiceClient _client;
        private readonly IMapper _mapper;

        public ClientRepository(ClientServiceClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<bool> RegisterClient(string username, string password, string email)
        {
            RegisterClientRequest request = new RegisterClientRequest
            {
                UserName = username,
                Password = password,
                Email = email,
            };

            RegisterClientResponse response = await _client.RegisterClientAsync(request);

            if (!response.IsSuccess)
            {
                throw new Exception(response.ErrorMessage);
            }

            return response.IsSuccess;
        }

        public async Task<string> Login(string email, string password)
        {
            AuthenticateClientRequest request = new AuthenticateClientRequest
            {
                Identifier = email,
                Password = password
            };

            AuthenticateClientResponse response = await _client.AuthenticateClientAsync(request);

            if (!response.IsSuccess)
            {
                throw new Exception("Invalid password or login");
            }

            return response.Token;
        }
    }
}
