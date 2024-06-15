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
        Task RemoveScooter(Guid id);
    }
}
