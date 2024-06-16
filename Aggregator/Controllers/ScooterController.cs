using Aggregator.Models;
using Aggregator.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Aggregator.Controllers
{
    [Route("api/v1/scooter")]
    public class ScooterController : Controller
    {

        private readonly IScooterService _scooterService;
        public ScooterController(IScooterService scooterService)
        {
            _scooterService = scooterService;
        }
        [HttpGet]
        public async Task<IActionResult> GetScooters([FromQuery] bool available = false)
        {
            try
            {
                if (available)
                {
                    var availableScooters = await _scooterService.GetAvailableScooters();
                    return Ok(availableScooters);
                }
                else
                {
                    var allScooters = await _scooterService.GetScooterList();
                    return Ok(allScooters);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented in this example)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddScooter(Scooter scooter)
        {
            if (scooter == null)
            {
                return BadRequest("Scooter object is null");
            }

            try
            {
                await _scooterService.AddedScooter(scooter);
                return CreatedAtAction(nameof(GetScooters), new { id = scooter.Id }, scooter);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented in this example)
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateScooterStatus(Guid id, [FromBody] bool status)
        {
            try
            {
                await _scooterService.UpdateScooterStatus(id, status);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented in this example)
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveScooter(Guid id)
        {
            try
            {
                await _scooterService.RemoveScooter(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented in this example)
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
