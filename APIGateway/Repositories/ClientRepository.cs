using static ClientAccount.ClientService;
using AutoMapper;
using ClientAccount;
using APIGateway.Models;
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
                throw new Exception(response.ErrorMessage);
            }

            return response.Token;
        }

        public async Task<ClientDto> GetPersonalCabinet(Guid id)
        {
            GetClientInfoRequest request = new GetClientInfoRequest
            {
                Id = id.ToString(),
            };

            GetClientInfoResponse response = await _client.GetClientInfoAsync(request);

            Client client = _mapper.Map<Client>(response);

            return new ClientDto
            {
                Id = client.Id,
                userName = client.userName,
                SessionIds = client.SessionIds,
            };

        }
        
        public async Task AddSession(Guid clientId,  Guid sessionId)
        {
            AddRentalSessionRequest request = new AddRentalSessionRequest
            {
                ClientId = clientId.ToString(),
                SessionId = sessionId.ToString(),
            };

            AddRentalSessionResponse response = await _client.AddRentalSessionAsync(request);

            if (!response.IsSuccess) {
                throw new Exception("Error adding a session");
            }
        }
    }
}
