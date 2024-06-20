using APIGateway.Interfaces.Repositories;
using APIGateway.Models;
using AutoMapper;
using RentalSession;
using static RentalSession.RentalSessionService;


namespace APIGateway.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly RentalSessionServiceClient _client;
        private readonly IMapper _mapper;

        public SessionRepository(RentalSessionServiceClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<Session> GetSessionInfo(Guid sessionId)
        {
            GetSessionInfoRequest request = new GetSessionInfoRequest { Id = sessionId.ToString() };

            GetSessionInfoResponse response = await _client.GetSessionInfoAsync(request);

            if (response == null)
            {
                throw new Exception("No session under such ID");
            }

            return _mapper.Map<Session>(response);
        }

        public async Task<bool> StartSession(Guid clientId, Guid scooterId)
        {
            StartSessionRequest request = new StartSessionRequest
            {
                ClientId = clientId.ToString(),
                ScooterId = scooterId.ToString()
            };

            StartSessionResponse response = await _client.StartSessionAsync(request);

            if (!response.IsSuccess)
            {
                throw new Exception("Error starting session");
            }

            return response.IsSuccess;
        }

        public async Task<bool> EndSession(Guid sessionId)
        {
            EndSessionRequest request = new EndSessionRequest { Id = sessionId.ToString() };

            EndSessionResponse response = await _client.EndSessionAsync(request);

            if (!response.IsSuccess)
            {
                throw new Exception("Error ending a session");
            }

            return response.IsSuccess;
        }

    }
}
