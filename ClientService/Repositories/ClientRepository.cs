using ClientService.Data;
using ClientService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientDbContext _context;

        public ClientRepository(ClientDbContext context)
        {
            _context = context;
        }

        public async Task<ClientEntity> GetClientByEmail(string email)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.email == email);
        }

        public async Task<ClientEntity> GetClientByUserName(string userName)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.userName == userName);
        }

        public async Task<bool> AddClient(ClientEntity client)
        {
            _context.Clients.Add(client);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
