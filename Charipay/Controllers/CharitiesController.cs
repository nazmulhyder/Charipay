using Charipay.Application.Commands.Charities;
using Charipay.Application.Queries.Charities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charipay.API.Controllers
{
    [ApiController] // ✅ Tells ASP.NET Core this is an API controller
    [Route("api/[controller]")] // ✅ Base route will be api/users
    public class CharitiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;

        public CharitiesController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;

        }

        [HttpPost("CreateCharity")]
        public async Task<IActionResult> CreateCharity([FromBody] CreateCharityCommand request, CancellationToken token)
        {
            var result = await _mediator.Send(request);

            if (result.Message.Contains("exists"))
                return BadRequest(result);

            return Ok(result);
            
        }

        [HttpPost("UpdateCharity")]
        public async Task<IActionResult> UpdateCharity([FromBody] UpdateCharityCommand request, CancellationToken token)
        {
            var result = await _mediator.Send(request);

            if (result.Message.Contains("exists"))
                return BadRequest(result);

            return Ok(result);

        }

        [HttpGet("GetAllCharity")]
        public async Task<IActionResult> GetAllCharity([FromQuery] GetAllCharityQuery query,CancellationToken token)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }


        [HttpDelete("DeleteCharity")]
        public async Task<IActionResult> DeleteCharity([FromQuery] DeleteCharityCommand query, CancellationToken token)
        {
            var result = await _mediator.Send(query);

            if (result.Message.Contains("exists"))
                return BadRequest(result);

            return Ok(result);
        }
    }
}
