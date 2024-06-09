using ClientAccount;
using ClientService.Repositories;
using Grpc.Core;
using ClientService.Entities;
using static ClientAccount.ClientService;

namespace ClientService.Services
{
    public class ClientServiceImpl : ClientServiceBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientServiceImpl(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public override async Task<AuthenticateClientResponse> AuthenticateClient(AuthenticateClientRequest request, ServerCallContext context)
        {
            ClientEntity client = null;

            if (IsValidEmail(request.Identifier))
            {
                client = await _clientRepository.GetClientByEmail(request.Identifier);
            }
            else
            {
                client = await _clientRepository.GetClientByUserName(request.Identifier);
            }

            if (client != null && client.password == request.Password)
            {
                return new AuthenticateClientResponse { IsSuccess = true };
            }

            return new AuthenticateClientResponse { IsSuccess = false, ErrorMessage = "Invalid credentials" };
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
