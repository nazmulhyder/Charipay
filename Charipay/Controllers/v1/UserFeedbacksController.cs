using Asp.Versioning;
using Charipay.Application.Commands.Admin.UserFeedback;
using Charipay.Application.Commands.Admin.Volunteer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charipay.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserFeedbacksController : Controller
    {
        private readonly IMediator _mediator;
        public UserFeedbacksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("give-feedback")]
        public async Task<IActionResult> CreateUserFeedback([FromBody] CreateFeedbackCommand cmd)
        {

            var result = await _mediator.Send(cmd);

            return Ok(result);
        }

    }
}
