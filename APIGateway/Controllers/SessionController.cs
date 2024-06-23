using APIGateway.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using APIGateway.Models;

namespace APIGateway.Controllers
{
    public class SessionController : ControllerBase
    {
        public readonly IClientRepository _clientRepository;
        public readonly ISessionRepository _sessionRepository;
        public readonly IScooterRepository _scooterRepository;

        public SessionController(IClientRepository clientRepository, 
            ISessionRepository sessionRepository, 
            IScooterRepository scooterRepository)
        {
            _clientRepository = clientRepository;
            _sessionRepository = sessionRepository;
            _scooterRepository = scooterRepository;
        }

        [HttpGet("ClientHistory")]

        public async Task<IActionResult> ClientHistory(List<Guid> sessionIds)
        {
            try
            {
                var sessionTasks = sessionIds.Select(_sessionRepository.GetSessionInfo);
                var sessions = await Task.WhenAll(sessionTasks);

                var scooterModelTasks = sessions.Select(s => _scooterRepository.GetScooterModel(s.ScooterId));
                var scooterModels = await Task.WhenAll(scooterModelTasks);

                var rentals = sessions.Zip(scooterModels, (session, model) => new Rental
                {
                    Id = session.Id,
                    ScooterModel = model,
                    StartTime = session.StartTime,
                    EndTime = session.EndTime,
                    RentalCost = session.RentalCost,
                }).ToList();

                return Ok(rentals);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while retrieving rental history.");
            }
        }


        [HttpPost("StartSession")]

        public async Task<IActionResult> StartSession(Guid clientId, Guid scooterId)
        {
            Guid sessionId = await _sessionRepository.StartSession(clientId, scooterId);


            if (sessionId == Guid.Empty)
            {
                return StatusCode(500, "An error occurred while starting a session.");
            }
            await _scooterRepository.AddSession(scooterId, sessionId);
            await _scooterRepository.UpdateScooterStatus(scooterId);
            await _clientRepository.AddSession(clientId, sessionId);
            return Ok(sessionId);
        }


        [HttpPost("EndSession")]
        public async Task<IActionResult> EndSession(Guid rentalId)
        {
            SessionDto sessionDto = await _sessionRepository.EndSession(rentalId);

            Guid scooterId = await _scooterRepository.GetScooterBySession(rentalId);

            await _scooterRepository.UpdateScooterStatus(scooterId);

            if (sessionDto == null)
            {
                return StatusCode(500, "An error occurred while ending a session.");
            }

            return Ok(sessionDto);
        }
    }
}
