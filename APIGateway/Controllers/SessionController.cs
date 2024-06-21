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
                    ScooterModel = await _scooterRepository.GetScooterModel(sessionId),
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
            bool check = await _sessionRepository.StartSession(clientId, scooterId);

            if (!check)
            {
                return StatusCode(500, "An error occurred while starting a session.");
            }

            return Ok("Session started");
        }

        public async Task<IActionResult> EndSession(Guid rentalId)
        {
            bool check = await _sessionRepository.EndSession(rentalId);

            if (!check)
            {
                return StatusCode(500, "An error occurred while ending a session.");
            }

            return Ok($"Session {rentalId} successfully ended");
        }
    }
}
