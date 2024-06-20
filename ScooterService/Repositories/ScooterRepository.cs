using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScooterService.Data;
using ScooterService.Entities;
using ScooterService.Models;

namespace ScooterService.Repositories
{
    public class ScooterRepository : IScooterRepository
    {
        private readonly ScooterDbContext _context;
        private readonly IMapper _mapper;

        public ScooterRepository(ScooterDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<Scooter>> GetAvailableScooters()
        {
            List<ScooterEntity> scooterEntities = await _context.Scooters.Where(s => s.Status).ToListAsync();
            List<Scooter> scooters = _mapper.Map<List<Scooter>>(scooterEntities);

            return scooters;
        }

        public async Task<List<Scooter>> GetAllScooters()
        {
            List<ScooterEntity> scooterEntities = await _context.Scooters.ToListAsync();
            List<Scooter> scooters = _mapper.Map<List<Scooter>>(scooterEntities);

            return scooters;
        }

        public async Task<bool> AddScooterAsync(Scooter scooter)
        {
            ScooterEntity scooterEntity = _mapper.Map<ScooterEntity>(scooter);
            await _context.Scooters.AddAsync(scooterEntity);
            await _context.SaveChangesAsync();

            var addedScooterEntity = await _context.Scooters.FindAsync(scooterEntity.Id);

            return addedScooterEntity != null;
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

        public async Task<string> GetScooterModel(Guid id)
        {
            ScooterEntity? scooterEntity = await _context.Scooters.FindAsync(id);
            if (scooterEntity == null)
            {
                throw new Exception("No scooter under such ID");
            }
            Scooter scooter = _mapper.Map<Scooter>(scooterEntity);

            return scooter.Model!;
        }

        public async Task<bool> UpdateScooterStatusAsync(Guid id)
        {
            ScooterEntity? scooter = await _context.Scooters.FindAsync(id);
            if (scooter == null)
            {
                return false;
            }

            scooter!.Status = !scooter.Status;
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

            scooterEntity.SessionIds?.Add(sessionId);
            scooterEntity.Status = false;

            _context.Scooters.Update(scooterEntity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
