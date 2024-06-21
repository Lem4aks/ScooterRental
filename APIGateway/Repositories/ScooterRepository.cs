using APIGateway.Interfaces.Repositories;
using APIGateway.Models;
using AutoMapper;
using ScooterInventoryGrpc;
using static ScooterInventoryGrpc.ScooterInventoryService;

namespace APIGateway.Repositories
{
    public class ScooterRepository : IScooterRepository
    {
        private readonly ScooterInventoryServiceClient _scooterClient;
        private readonly IMapper _mapper;

        public ScooterRepository(ScooterInventoryServiceClient scooterClient, IMapper mapper)
        {
            _scooterClient = scooterClient;
            _mapper = mapper;
        }

        public async Task AddScooter(string model)
        {
            AddScooterRequest request = new AddScooterRequest
            {
                Model = model
            };

            AddScooterResponse response = await _scooterClient.AddScooterAsync(request);

            if (!response.IsSuccess)
            {
                throw new Exception("Failed to add scooter.");
            }
        }

        public async Task<List<Scooter>> GetScooters()
        {
            GetAllScootersRequest request = new GetAllScootersRequest();

            var response = await _scooterClient.GetAllScootersAsync(request);

            var scooterMessages = response.Scooters;

            List<Scooter> scooters = _mapper.Map<List<Scooter>>(scooterMessages);

            return scooters;

        }

        public async Task<List<Scooter>> GetAvailableScooters()
        {
            GetAvailableScootersRequest request = new GetAvailableScootersRequest();

            GetAvailableScootersResponse response = await _scooterClient.GetAvailableScootersAsync(request);

            var scooterMessages = response.Scooters;

            List<Scooter> scooters = _mapper.Map<List<Scooter>>(scooterMessages);

            return scooters;
        }
        public async Task UpdateScooterStatus(Guid id)
        {
            UpdateScooterStatusRequest request = new UpdateScooterStatusRequest { Id = id.ToString() };


            UpdateScooterStatusResponse response = await _scooterClient.UpdateScooterStatusAsync(request);

            if (!response.IsSuccess)
            {
                throw new Exception("Failed to update scooter status.");
            }
        }

        public async Task<Guid> GetScooterBySession(Guid sessionId)
        {
            GetScooterIdBySessionRequest request = new GetScooterIdBySessionRequest { SessionId = sessionId.ToString() };

            GetScooterIdBySessionResponse response = await _scooterClient.GetScooterIdBySessionAsync(request);

            if (response.ScooterId == null)
            {
                throw new Exception("Failed to find a scooter.");
            }

            return Guid.Parse(response.ScooterId);
        }

        public async Task RemoveScooter(Guid id)
        {
            DeleteScooterRequest request = new DeleteScooterRequest { Id = id.ToString() };

            DeleteScooterResponse reponse = await _scooterClient.DeleteScooterAsync(request);
            if (!reponse.IsSuccess)
            {
                throw new Exception("Failed to delete scooter.");
            }
        }

        public async Task<string> GetScooterModel(Guid scooterId)
        {
            GetScooterModelRequest request = new GetScooterModelRequest { Id = scooterId.ToString() };

            GetScooterModelResponse response = await _scooterClient.GetScooterModelAsync(request);

            return response.Model;
        }

        public async Task AddSession(Guid scooterId, Guid sessionId)
        {
            AddSessionRequest request = new AddSessionRequest
            {
                SessionId = sessionId.ToString(),
                ScooterId = scooterId.ToString(),
            };

            AddSessionResponse response = await _scooterClient.AddSessionAsync(request);

            if (!response.IsSuccess)
            {
                throw new Exception("Failed to add a session.");
            }
        }
    }
}
