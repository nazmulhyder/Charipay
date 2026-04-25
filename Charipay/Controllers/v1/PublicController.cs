using Asp.Versioning;
using Charipay.Application.Queries.Home;
using Charipay.Application.Queries.Volunteer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charipay.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class PublicController : ControllerBase
        {
        private readonly IMediator _mediator;
        public PublicController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("home-stats")]
        public async Task<IActionResult> GetHomeStats([FromQuery] GetHomeStatsQuery query, CancellationToken token)
        {
            var result = await _mediator.Send(query);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

    }
}
