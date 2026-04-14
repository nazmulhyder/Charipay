using Asp.Versioning;
using Charipay.Application.Queries.Admin.Volunteer;
using Charipay.Application.Queries.Volunteer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charipay.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VolunteersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VolunteersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("opportunities")]
        public async Task<IActionResult> GetPagedVolunteerOpportunities([FromQuery] GetVolunteerOpportunitiesQuery query, CancellationToken token)
        {
            var result = await _mediator.Send(query);

            if (result.Message.Contains("exists"))
                return BadRequest(result);

            return Ok(result);
        }

    }
}
