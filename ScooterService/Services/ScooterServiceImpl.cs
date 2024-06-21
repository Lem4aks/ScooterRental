using Grpc.Core;
using ScooterInventoryGrpc;
using ScooterService.Entities;
using ScooterService.Models;
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
            List<Scooter> availableScooters = await _repository.GetAvailableScooters();
            GetAvailableScootersResponse response = new GetAvailableScootersResponse();
            response.Scooters.AddRange(availableScooters.Select(s => new ScooterMessage
            {
                Id = s.Id.ToString(),
                Model = s.Model,
                Status = s.Status,
                SessionIds = { s.SessionIds?.Select(id => id.ToString()) }
            }));
            return response;
        }

        public override async Task<GetAllScootersResponse> GetAllScooters(GetAllScootersRequest request, ServerCallContext context)
        {
            List<Scooter> allScooters = await _repository.GetAllScooters();
            GetAllScootersResponse response = new GetAllScootersResponse();
            response.Scooters.AddRange(allScooters.Select(s => new ScooterMessage
            {
                Id = s.Id.ToString(),
                Model = s.Model,
                Status = s.Status,
                SessionIds = { s.SessionIds?.Select(id => id.ToString()) }
            }));

            return response;
        }

        public override async Task<AddSessionResponse> AddSession(AddSessionRequest request, ServerCallContext context)
        {
            bool check = await _repository.AddSession(Guid.Parse(request.SessionId), Guid.Parse(request.ScooterId));

            if (check == false)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Unable to find a scooter with this ID"));
            }

            return new AddSessionResponse { IsSuccess = true };
        }
        public override async Task<AddScooterResponse> AddScooter(AddScooterRequest request, ServerCallContext context)
        {
            Scooter newScooter = Scooter.CreateScooter(request.Model);
            bool check = await _repository.AddScooterAsync(newScooter);

            if (check == true)
            {
                return new AddScooterResponse { IsSuccess = true };
            }
            else
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Unable to add a scooter"));
        }

        public override async Task<DeleteScooterResponse> DeleteScooter(DeleteScooterRequest request, ServerCallContext context)
        {
            bool success = await _repository.DeleteScooterAsync(Guid.Parse(request.Id));
            return new DeleteScooterResponse { IsSuccess = true };
        }

        public override async Task<UpdateScooterStatusResponse> UpdateScooterStatus(UpdateScooterStatusRequest request, ServerCallContext context)
        {
            bool success = await _repository.UpdateScooterStatusAsync(Guid.Parse(request.Id));
            return new UpdateScooterStatusResponse { IsSuccess = success };
        }

        public override async Task<GetScooterModelResponse> GetScooterModel(GetScooterModelRequest request, ServerCallContext context)
        {
            string scooterModel = await _repository.GetScooterModel(Guid.Parse(request.Id));
            if (string.IsNullOrEmpty(scooterModel))
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Scooter model under such ID is not found"));
            }
            return new GetScooterModelResponse { Model = scooterModel };

        }
    }
}
