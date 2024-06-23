using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentalService.Data;
using RentalService.Models;
using RentalService.Entities;
using RentalService.Exceptions;
using System.Data;

namespace RentalService.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly RentalDbContext _context;
        const decimal rentalCostPerMinute = 0.2m;
        private readonly IMapper _mapper;
        public RentalRepository(RentalDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Session> GetSessionInfo (Guid id)
        {
            bool sessionExists = await _context.Sessions.AnyAsync(s => s.Id == id);
            if (sessionExists)
            {
                SessionEntity? sessionEntity = await _context.Sessions.FindAsync(id);

                return _mapper.Map<Session>(sessionEntity);
            }
            else
            {
                throw new SessionNotFoundException($"No session found with ID: {id}");
            }
        }
        public async Task<bool> HasUnfinishedSession(Guid clientId)
        {
            bool hasUnfinishedSessions = _context.Sessions
                    .Any(s => s.ClientId == clientId && s.EndTime == null);

            return hasUnfinishedSessions;
        }

        public async Task<Session> StartSession(Guid clientId, Guid scooterId) 
        {
            DateTimeOffset startTime = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(3));

            Session session = Session.CreateSession(clientId, scooterId, startTime.DateTime);

            SessionEntity sessionEntity = _mapper.Map<SessionEntity>(session);

            await _context.Sessions.AddAsync(sessionEntity);
            await _context.SaveChangesAsync();

            return session;
        }

        public async Task<Session> EndSession(Guid sessionId)
        {
            DateTimeOffset endTime = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(3));
            bool sessionExists = await _context.Sessions.AnyAsync(s => s.Id == sessionId);
            if (!sessionExists)
            {
                throw new SessionNotFoundException($"No session found with ID: {sessionId}");
            }
            SessionEntity? sessionToEnd = await _context.Sessions.FindAsync(sessionId);
            if (sessionToEnd == null)
            {
                throw new SessionNotFoundException($"No session found with ID: {sessionId}");
            }
            sessionToEnd!.EndTime = endTime.DateTime.ToUniversalTime();

            TimeSpan rentalDuration = sessionToEnd.EndTime.Value - sessionToEnd.StartTime;

            double totalMinutes = rentalDuration.TotalMinutes;
            decimal rentalCost = (decimal)totalMinutes * rentalCostPerMinute;
            sessionToEnd.RentalCost = rentalCost;

            _context.Sessions.Update(sessionToEnd);
            await _context.SaveChangesAsync();

            Session session = _mapper.Map<Session>(sessionToEnd);

            return session;
           
        }
    }
}
