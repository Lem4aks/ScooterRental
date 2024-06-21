using APIGateway.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using APIGateway.Models;
using APIGateway.JWT;

namespace APIGateway.Controllers
{
    [Route("api/v1/client")]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _repository;
        private readonly IJwtProvider _jwtProvider;

        public ClientController(IClientRepository repository, IJwtProvider jwtProvider)
        {
            _repository = repository;
            _jwtProvider = jwtProvider;
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

            if (token == string.Empty) {
                return BadRequest(token);
            }

            Response.Cookies.Append("cookies", token);

            Guid id = Guid.Parse(_jwtProvider.GetIdFromToken(token));

            ClientDto clientDto = await _repository.GetPersonalCabinet(id);

            clientDto.Token = token;

            return Ok(clientDto);
        }

    }
}
