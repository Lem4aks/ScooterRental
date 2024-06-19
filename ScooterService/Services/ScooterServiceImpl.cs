using Grpc.Core;
using ScooterInventoryGrpc;
using ScooterService.Entities;
using ScooterService.Repositories;

namespace ScooterService.Services
{
    public class ScooterServiceImpl : ScooterInventoryService.ScooterInventoryServiceBase
    {
        private readonly IScooterRepository _repository;

        public ScooterServiceImpl(IScooterRepository repository)
        {
            _repository = repository;
        }

        public override async Task<GetAvailableScootersResponse> GetAvailableScooters(GetAvailableScootersRequest request, ServerCallContext context)
        {
            List<ScooterEntity> availableScooters = await _repository.GetAvailableScooters();
            GetAvailableScootersResponse response = new GetAvailableScootersResponse();
            response.Scooters.AddRange(availableScooters.Select(s => new ScooterMessage
            {
                Id = s.Id.ToString(),
                Model = s.Model,
                Status = s.Status,
                SessionIds = { s.SessionIds.Select(id => id.ToString()) }
            }));
            return response;
        }

        public override async Task<GetAllScootersResponse> GetAllScooters(GetAllScootersRequest request, ServerCallContext context)
        {
            List<ScooterEntity> allScooters = await _repository.GetAllScooters();
            GetAllScootersResponse response = new GetAllScootersResponse();
            response.Scooters.AddRange(allScooters.Select(s => new ScooterMessage
            {
                Id = s.Id.ToString(),
                Model = s.Model,
                Status = s.Status,
                SessionIds = { s.SessionIds.Select(id => id.ToString()) }
            }));

            return response;
        }

        public override async Task<AddScooterResponse> AddScooter(AddScooterRequest request, ServerCallContext context)
        {
            var scooterEntity = new ScooterEntity
            {
                Id = Guid.Parse(request.Scooter.Id),
                Model = request.Scooter.Model,
                Status = request.Scooter.Status,
                SessionIds = request.Scooter.SessionIds.Select(id => Guid.Parse(id)).ToList()
            };
            await _repository.AddScooterAsync(scooterEntity);
            return new AddScooterResponse { IsSuccess = true };
        }

        public override async Task<DeleteScooterResponse> DeleteAScooter(DeleteScooterRequest request, ServerCallContext context)
        {
            bool success = await _repository.DeleteScooterAsync(Guid.Parse(request.Id));
            return new DeleteScooterResponse { IsSuccess = true };
        }

        public override async Task<UpdateScooterStatusResponse> UpdateScooterStatus(UpdateScooterStatusRequest request, ServerCallContext context)
        {
            bool success = await _repository.UpdateScooterStatusAsync(Guid.Parse(request.Id));
            return new UpdateScooterStatusResponse { IsSuccess = success };
        }
    }
}
