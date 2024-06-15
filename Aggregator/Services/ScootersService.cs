using Aggregator.Models;
using ScooterInventoryGrpc;
using Grpc.Net.Client;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Aggregator.Services
{
    public class ScootersService : IScooterService
    {
        private readonly ScooterInventoryService.ScooterInventoryServiceClient _scooterClient;

        public ScootersService(ScooterInventoryService.ScooterInventoryServiceClient scooterClient)
        {
            _scooterClient = scooterClient;
        }

        public async Task AddedScooter(Models.Scooter scooter)
        {
            var scooterProto = new ScooterInventoryGrpc.Scooter
            {
                Id = scooter.Id.ToString(),
                Model = scooter.Model,
                Status = scooter.Status,
                SessionIds = { scooter.SessionIds.Select(id => id.ToString()) }
            };

            var request = new AddScooterRequest
            {
                Scooter = scooterProto
            };

            var response = await _scooterClient.AddScooterAsync(request);
            if (!response.IsSuccess)
            {
                throw new Exception("Failed to add scooter.");
            }
        }

        public async Task<List<Models.Scooter>> GetScooterList()
        {
            var request = new GetAllScootersRequest();
            var response = await _scooterClient.GetAllScootersAsync(request);

            return response.Scooters.Select(se => new Models.Scooter
            {
                Id = Guid.Parse(se.Id),
                Model = se.Model,
                Status = se.Status,
                SessionIds = se.SessionIds.Select(Guid.Parse).ToList()
            }).ToList();
        }

        public async Task RemoveScooter(Guid id)
        {
            var request = new DeleteScooterRequest
            {
                Id = id.ToString()
            };

            var response = await _scooterClient.DeleteAScooterAsync(request);
            if (!response.IsSuccess)
            {
                throw new Exception("Failed to delete scooter.");
            }
        }
    }
}
