using ClientAccount;
using ClientService.Repositories;
using Grpc.Core;
using ClientService.Entities;
using static ClientAccount.ClientService;
using System.Security.Claims;
using ClientService.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ClientService.Models;
using ClientService.Auth;
using Microsoft.AspNetCore.Identity;

namespace ClientService.Services
{
    public class ClientServiceImpl : ClientServiceBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IPasswordHasher _passwordHasher;

        public ClientServiceImpl(IClientRepository clientRepository, IPasswordHasher passwordHasher)
        {
            _clientRepository = clientRepository;
            _passwordHasher = passwordHasher;
        }

        /*public override async Task<AuthenticateClientResponse> AuthenticateClient(AuthenticateClientRequest request, ServerCallContext context)
        {
          
        } */

        public override async Task<RegisterClientResponse> RegisterClient(RegisterClientRequest request, ServerCallContext context)
        {
            var hashedPassword = _passwordHasher.Generate(request.Password);

            Client client = Client.Create(request.UserName, hashedPassword, request.Email);

            bool check = await _clientRepository.AddClient(client);

            if (check)
            {
                return new RegisterClientResponse { IsSuccess = true, ErrorMessage = string.Empty };
            }

            else
            {
                return new RegisterClientResponse { IsSuccess = false, ErrorMessage = "Failed to register a user" };
            }

        }

    }
}
