using Aggregator.Models;
using ScooterService.Entities;
using ScooterService.Repositories;


namespace Aggregator.Services
{
    public class ScootersService : IScooterService
    {
        private readonly IScooterRepository _repository;

        public ScootersService(IScooterRepository repository)
        {
            _repository = repository;
        }

        public async Task AddScooter(Scooter scooter)
        {
            var scooterEntity = new ScooterEntity
            {
                Id = scooter.Id,
                Model = scooter.Model,
                Status = scooter.Status,
                SessionIds = scooter.SessionIds
            };

            await _repository.AddScooterAsync(scooterEntity);
        }

        public async Task<List<Scooter>> GetScooterList()
        {
            var scooterEntities = await _repository.GetAllScooters();
            return scooterEntities.Select(se => new Scooter
            {
                Id = se.Id,
                Model = se.Model,
                Status = se.Status,
                SessionIds = se.SessionIds
            }).ToList();
        }

        public async Task RemoveScooter(Guid id)
        {
            await _repository.DeleteScooterAsync(id);
        }
    }
}
