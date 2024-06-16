using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aggregator.Models;

namespace Aggregator.Services
{
    public interface IScooterService
    {
        Task AddedScooter(Scooter scooter);
        Task<List<Scooter>> GetScooterList();
        Task<List<Scooter>> GetAvailableScooters();
        public Task UpdateScooterStatus(Guid id, bool status);
        Task RemoveScooter(Guid id);
    }
}
