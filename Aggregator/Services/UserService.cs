using Aggregator.Models;
using ClientService.Entities;
using ClientService.Repositories;

namespace Aggregator.Services
{
    public class UserService : IUserService
    {
        private readonly IClientRepository _clientRepository;

        public UserService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task Login(User user)
        {
            ClientEntity clientEntity = await _clientRepository.GetClientByUserName(user.userName);

            if (clientEntity != null)
            {
                await Registration(user);
            }
            else
            {
                var newClient = new ClientEntity
                {
                    Id = user.Id,
                    userName = user.userName,
                    password = user.password,
                    email = user.email,
                    SessionIds = user.SessionIds
                };

                await _clientRepository.AddClient(newClient);
            }
        }

        public void Logout()
        {
            // не знаю еще нужен он или нет
        }

        public async Task<bool> Registration(User user)
        {
            var clientEntity = new ClientEntity
            {
                Id = user.Id,
                userName = user.userName,
                password = user.password,
                email = user.email,
                SessionIds = user.SessionIds
            };

            return await _clientRepository.AddClient(clientEntity);
        }
    }
}
