using Grpc.Core;
using RentalService.Entities;
using RentalService.Models;
using RentalService.Repositories;
using RentalSession;

namespace RentalService.Services
{
    public class RentalServiceImpl : RentalSessionService.RentalSessionServiceBase
    {
        private readonly IRentalRepository _repository;

        public RentalServiceImpl(IRentalRepository repository)
        {
            _repository = repository;
        }

        public override async Task<GetSessionInfoResponse> GetSessionInfo(GetSessionInfoRequest request, ServerCallContext context)
        {
            Session session = await _repository.GetSessionInfo(Guid.Parse(request.Id));

            if (session != null)
            {
                GetSessionInfoResponse response = new GetSessionInfoResponse
                {
                    SessionMessage = new SessionMessage
                    {
                        Id = session.Id.ToString(),
                        ClientId = session.ClientId.ToString(),
                        ScooterId = session.ScooterId.ToString(),
                        StartTime = session.StartTime.ToString("O"),
                        EndTime = session.EndTime?.ToString("O") ?? "",
                        RentalCost = (double)session.RentalCost
                    }
                };
                return response;
            }
            else
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Session not found"));
            }
        }
        public override async Task<StartSessionResponse> StartSession(StartSessionRequest request, ServerCallContext context)
        {
            Guid clientId = Guid.Parse(request.ClientId);
            Guid scooterId = Guid.Parse(request.ScooterId);

            Session sessionToStart = await _repository.StartSession(clientId, scooterId);

            if (sessionToStart != null)
            {
                return new StartSessionResponse { IsSuccess = true, SessionId = sessionToStart.Id.ToString() };
            }

            else
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Unable to start a session, check the ID`s"));
            }
        }

        public override async Task<EndSessionResponse> EndSession(EndSessionRequest request, ServerCallContext context)
        {
            Guid sessionId = Guid.Parse(request.Id);

            Session sessionToEnd = await _repository.EndSession(sessionId);

            if (sessionToEnd != null)
            {
                return new EndSessionResponse { IsSuccess = true };
            }
            else
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Unable to end a session, check session ID"));
        }
    }
}
