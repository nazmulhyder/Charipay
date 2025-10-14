using Charipay.Application.Queries.Admin.Dashboard.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charipay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminDashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("UserList")]
        public async Task<IActionResult> UserList([FromQuery]GetUserListQuery query,CancellationToken token)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
