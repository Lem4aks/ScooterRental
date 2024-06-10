using Grpc.Core;
using ScooterInventory;
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
            List<Entities.ScooterEntity> availableScooters = await _repository.GetAvailableScooters();
            GetAvailableScootersResponse response = new GetAvailableScootersResponse();
            response.Scooters.AddRange(availableScooters.Select(s => new Scooter
            {
                Id = s.Id.ToString(),
                Model = s.Model,
                Status = s.Status,
                SessionIds = { s.SessionIds.Select(id => id.ToString()) }
            }));
            return response;
        }

        public override async Task<AddNewScooterResponse> AddANewScooter(Scooter request, ServerCallContext context)
        {
            var scooterEntity = new ScooterEntity
            {
                Id = Guid.Parse(request.Id),
                Model = request.Model,
                Status = request.Status,
                SessionIds = request.SessionIds.Select(id => Guid.Parse(id)).ToList()
            };
            await _repository.AddScooterAsync(scooterEntity);
            var response = new AddNewScooterResponse { IsSuccess = true };
            return response;
        }

        public override async Task<DeleteScooterResponse> DeleteAScooter(DeleteScooterRequest request, ServerCallContext context)
        {
            var success = await _repository.DeleteScooterAsync(Guid.Parse(request.Id));
            return new DeleteScooterResponse { IsSuccess = true };
        }

        public override async Task<UpdateScooterStatusResponse> UpdateScooterStatus(UpdateScooterStatusRequest request, ServerCallContext context)
        {
            var success = await _repository.UpdateScooterStatusAsync(Guid.Parse(request.Id), request.Status);
            return new UpdateScooterStatusResponse { IsSuccess = success };
        }
    }
}
