using Charipay.Application.Commands.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charipay.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IMediator _mediator, ILogger<AuthController> _logger) : ControllerBase
    {
        // POST: api/auth/login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);

        }
    }
}
