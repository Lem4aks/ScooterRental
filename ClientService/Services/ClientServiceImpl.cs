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
using ClientService.JWT;

namespace ClientService.Services
{
    public class ClientServiceImpl : ClientServiceBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public ClientServiceImpl(
            IClientRepository clientRepository, 
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider)
            
        {
            _clientRepository = clientRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public override async Task<AuthenticateClientResponse> AuthenticateClient(AuthenticateClientRequest request, ServerCallContext context)
        {
          Client client = await _clientRepository.GetClientByEmail(request.Identifier);


          var result = _passwordHasher.Verify(request.Password, client.password);

            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvider.GenerateToken(client);

            return new AuthenticateClientResponse { IsSuccess = true, Token = token };
        } 

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
