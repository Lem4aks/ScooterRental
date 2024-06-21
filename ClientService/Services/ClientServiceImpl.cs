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

        public override async Task<GetClientInfoResponse> GetClientInfo(GetClientInfoRequest request, ServerCallContext context)
        {
            Client client = await _clientRepository.GetClientById(Guid.Parse(request.Id));

            if (client == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Client not found"));
            }

            ClientMessage clientMessage = new ClientMessage
            {
                Email = client.email,
                UserName = client.userName,
                Password = client.password,
            };

            clientMessage.SessionIds.AddRange(client.SessionIds.Select(id => id.ToString()));
            GetClientInfoResponse response = new GetClientInfoResponse
            {
                Client = clientMessage
            };

            return response;

            
        }

        public override async Task<AddRentalSessionResponse> AddRentalSession(AddRentalSessionRequest request, ServerCallContext context)
        {
            bool check = await _clientRepository.AddSession(Guid.Parse(request.ClientId), Guid.Parse(request.SessionId));

            if (!check)
            {
                return new AddRentalSessionResponse { IsSuccess = false };
            }

            return new AddRentalSessionResponse { IsSuccess = true };
        }



        public override async Task<AuthenticateClientResponse> AuthenticateClient(AuthenticateClientRequest request, ServerCallContext context)
        {
          Client client = await _clientRepository.GetClientByEmail(request.Identifier);

            if (client == null)
            {
                return new AuthenticateClientResponse { IsSuccess = false, Token = string.Empty , ErrorMessage = "No user found with this email" };
            }

          var result = _passwordHasher.Verify(request.Password, client.password);

            if (result == false)
            {
                return new AuthenticateClientResponse { IsSuccess = false, Token = string.Empty, ErrorMessage = "Wrong password" };
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
