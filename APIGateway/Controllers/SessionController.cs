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
        
        public async Task<IActionResult> ClientHistory(Guid clientId)
        {
            ClientDto clientDto = await _clientRepository.GetPersonalCabinet(clientId);

            List<Rental> rentals = new List<Rental>();

            foreach (Guid sessionId in clientDto.SessionIds) { 
                Session session = await _sessionRepository.GetSessionInfo(sessionId);
                rentals.Add(new Rental
                {
                    Id = sessionId,
                    ScooterModel = await _scooterRepository.GetScooterModel(session.ScooterId),
                    StartTime = session.StartTime,
                    EndTime = session.EndTime,
                    RentalCost = session.RentalCost,    
                });
            }

            if (rentals == null) {
                return StatusCode(500, "An error occurred while retrieving rental history.");
            }

            return Ok(rentals);
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
            return Ok("Session started");
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
