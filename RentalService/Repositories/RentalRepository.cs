using Microsoft.EntityFrameworkCore;
using RentalService.Data;
using RentalService.Entities;
using RentalService.Exceptions;
using System.Data;

namespace RentalService.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly RentalDbContext _context;
        const decimal rentalCostPerMinute = 0.2m;
        public RentalRepository(RentalDbContext context)
        {
            _context = context;
        }

        public async Task<SessionEntity> GetSessionInfo (Guid id)
        {
            bool sessionExists = await _context.Sessions.AnyAsync(s => s.Id == id);
            if (sessionExists)
            {
                SessionEntity? session = await _context.Sessions.FindAsync(id);

                return session!;
            }
            else
            {
                throw new SessionNotFoundException($"No session found with ID: {id}");
            }
        }

        public async Task<SessionEntity> StartSession(Guid sessionId, Guid clientId, Guid scooterId) 
        {
            DateTimeOffset startTime = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(3));
            SessionEntity startedSession = new SessionEntity
            {
                Id = sessionId,
                ClientId = clientId,
                ScooterId = scooterId,
                StartTime = startTime.DateTime,
            };

            await _context.Sessions.AddAsync(startedSession);
            await _context.SaveChangesAsync();

            return startedSession;
        }

        public async Task<SessionEntity> EndSession(Guid sessionId)
        {
            DateTimeOffset endTime = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(3));
            bool sessionExists = await _context.Sessions.AnyAsync(s => s.Id == sessionId);
            if (sessionExists) {
                SessionEntity? sessionToEnd = await _context.Sessions.FindAsync(sessionId);

                sessionToEnd!.EndTime = endTime.DateTime;

                TimeSpan rentalDuration = sessionToEnd.EndTime.Value - sessionToEnd.StartTime;

                double totalMinutes = rentalDuration.TotalMinutes;
                decimal rentalCost = (decimal)totalMinutes * rentalCostPerMinute;
                sessionToEnd.RentalCost = rentalCost;

                _context.Sessions.Update(sessionToEnd);

                await _context.SaveChangesAsync();

                return sessionToEnd;
            }
            else
            {
                throw new SessionNotFoundException($"No session found with ID: {sessionId}");
            }

        }
    }
}
