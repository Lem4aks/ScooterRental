using ClientService.Models;
using ClientService.Data;
using ClientService.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ClientService.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientDbContext _context;
        private readonly IMapper _mapper;

        public ClientRepository(ClientDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Client> GetClientByEmail(string email)
        {
            ClientEntity clientEntity = await _context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.email == email) ?? throw new Exception("Wrong email");

            return _mapper.Map<Client>(clientEntity); 
        }

        public async Task<Client> GetClientByUserName(string userName)
        {
            ClientEntity clientEntity = await _context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.userName == userName) ?? throw new Exception("Wrong username");

            return _mapper.Map<Client>(clientEntity);
        }

        public async Task<bool> AddClient(Client client)
        {
            ClientEntity clientEntity = _mapper.Map<ClientEntity>(client);

            await _context.Clients.AddAsync(clientEntity);
            await _context.SaveChangesAsync();

            var addedClientEntity = await _context.Clients.FindAsync(clientEntity.Id);

            return addedClientEntity != null;

        }

        public async Task<bool> AddSession(Guid clientId, Guid sessionId)
        {
            ClientEntity? clientEntity = await _context.Clients.FindAsync(clientId);

            if (clientEntity == null)
            {
                return false;
            }

            clientEntity.SessionIds?.Add(sessionId);

            _context.Clients.Update(clientEntity);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RegisterClientAsync(Client client)
        {
            if (await _context.Clients.AnyAsync(c => c.email == client.email || c.userName == client.userName))
            {
                return false;
            }

            ClientEntity clientEntity = _mapper.Map<ClientEntity>(client);

            await _context.Clients.AddAsync(clientEntity);
            await _context.SaveChangesAsync();
            return true;
        } 


        /*public async Task<Client> AuthenticateClientAsync(string identifier, string password)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => (c.email == identifier || c.userName == identifier) && c.password == password);
        } */
    }
}
