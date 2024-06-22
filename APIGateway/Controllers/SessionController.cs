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


            List<Task<Session>> sessionTasks = new List<Task<Session>>();

            List<Task<string>> scooterModelTasks = new List<Task<string>>();

            foreach (Guid sessionId in clientDto.SessionIds)
            {
                sessionTasks.Add(_sessionRepository.GetSessionInfo(sessionId));
            }

            Session[] sessions = await Task.WhenAll(sessionTasks);

            foreach (var session in sessions)
            {
                scooterModelTasks.Add(_scooterRepository.GetScooterModel(session.ScooterId));
            }

            string[] scooterModels = await Task.WhenAll(scooterModelTasks);
            List<Rental> rentals = new List<Rental>();

            for (int i = 0; i < sessions.Length; i++)
            {
                rentals.Add(new Rental
                {
                    Id = clientDto.SessionIds[i],
                    ScooterModel = scooterModels[i],
                    StartTime = sessions[i].StartTime,
                    EndTime = sessions[i].EndTime,
                    RentalCost = sessions[i].RentalCost,
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
