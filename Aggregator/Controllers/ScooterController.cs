using APIGateway.Interfaces.Repositories;
using APIGateway.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Controllers
{
    [Route("api/v1/scooter")]
    public class ScooterController : ControllerBase
    {
        private readonly IScooterRepository _repository;

        public ScooterController(IScooterRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetScooters()
        {
            List<Scooter> scooters = await _repository.GetScooters();

            return Ok(scooters);
        }


        [HttpPost]

        public async Task<IActionResult> AddScooter(string model)
        {
            await _repository.AddScooter(model);

            return Ok();
        }

        [HttpDelete]

        public async Task<IActionResult> RemoveScooter(Guid id)
        {
            await _repository.RemoveScooter(id);

            return Ok();
        }
    }
}
