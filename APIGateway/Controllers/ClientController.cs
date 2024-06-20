using APIGateway.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Controllers
{
    [Route("api/v1/client")]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _repository;

        public ClientController(IClientRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("Register")]

        public async Task<IActionResult> RegisterClient(string userName, string password, string email)
        {
            await _repository.RegisterClient(userName, password, email);

            return Ok();
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login(string email, string password)
        {
            var token = await _repository.Login(email, password);

            return Ok(token);
        }

    }
}
