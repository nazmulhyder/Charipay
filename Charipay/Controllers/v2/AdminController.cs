using Asp.Versioning;
using Charipay.Application.Queries.Admin.Dashboard.Users;
using Charipay.Application.Queries.Charities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charipay.API.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("UserList")]
        public async Task<IActionResult> UserList([FromQuery]GetUserListQuery query,CancellationToken token)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("GetAllCharity")]
        public async Task<IActionResult> GetAllCharity([FromQuery] GetAllCharityQuery query, CancellationToken token)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
