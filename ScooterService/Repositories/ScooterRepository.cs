using Microsoft.EntityFrameworkCore;
using ScooterService.Data;
using ScooterService.Entities;

namespace ScooterService.Repositories
{
    public class ScooterRepository : IScooterRepository
    {
        private readonly ScooterDbContext _context;

        public ScooterRepository(ScooterDbContext context)
        {
            _context = context;
        }

        public async Task<List<ScooterEntity>> GetAvailableScooters()
        {
            return await _context.Scooters.Where(s => s.Status).ToListAsync();
        }

        public async Task<List<ScooterEntity>> GetAllScooters()
        {
            return await _context.Scooters.ToListAsync();
        }

        public async Task AddScooterAsync(ScooterEntity scooter)
        {
            _context.Scooters.Add(scooter);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteScooterAsync(Guid id)
        {
            ScooterEntity? scooter = await _context.Scooters.FindAsync(id);
            if (scooter == null)
            {
                return false;
            }

            _context.Scooters.Remove(scooter);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateScooterStatusAsync(Guid id)
        {
            ScooterEntity? scooter = await _context.Scooters.FindAsync(id);
            if (scooter == null)
            {
                return false;
            }

            scooter!.Status = true;
            _context.Scooters.Update(scooter);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddSession(Guid sessionId, Guid scooterId)
        {
            ScooterEntity? scooterEntity = await _context.Scooters.FindAsync(scooterId);

            if (scooterEntity == null)
            {
                return false;
            }

            scooterEntity.SessionIds.Add(sessionId);
            scooterEntity.Status = false;

            _context.Scooters.Update(scooterEntity);
            await _context.SaveChangesAsync();

            return true;
    }
    }
}
