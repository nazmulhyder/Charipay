using Asp.Versioning;
using Charipay.Application.Commands.Admin.Volunteer;
using Charipay.Application.Commands.Campaigns;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Admin.Volunteer;
using Charipay.Application.Queries.Admin.Dashboard.Users;
using Charipay.Application.Queries.Admin.Volunteer;
using Charipay.Application.Queries.Campaigns;
using Charipay.Application.Queries.Charities;
using Charipay.Application.Queries.Dashboard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charipay.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
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

        [HttpGet("Dashboard")]
        public async Task<IActionResult> GetAllCharity()
        {
            var result = await _mediator.Send(new GetDashboardQuery());

            return Ok(result);
        }

        [HttpPost("campaigns/create-volunteer-tasks")]
        public async Task<IActionResult> CreateVolunteerTask([FromBody] CreateVolunteerTaskCommand cmd)
        {

            var result = await _mediator.Send(cmd);

            return Ok(result);  
        }

        [HttpPost("UpdateVolunteerTask")]
        public async Task<IActionResult> UpdateVolunteerTask([FromBody] UpdateVolunteerTaskCommand request, CancellationToken token)
        {
            var result = await _mediator.Send(request);

            if (result.Message.Contains("exists"))
                return BadRequest(result);

            return Ok(result);

        }

        [HttpGet("volunteer/volunteer-task-list")]
        public async Task<IActionResult> GetPagedVolunteerTasks([FromQuery] GetPagedVolunteerTasksQuery query, CancellationToken token)
        {
            var result = await _mediator.Send(query);

            if (result.Message.Contains("exists"))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("volunteer/application/requests")]
        public async Task<IActionResult> GetPagedVolunteerApplicationRequests([FromQuery] GetVolunteerApplicationRequestsQuery query, CancellationToken token)
        {
            var result = await _mediator.Send(query);

            if (result.Message.Contains("exists"))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("review-volunteer-applications")]
        public async Task<IActionResult> ReviewVolunteerApplication([FromBody] ReviewVolunteerApplicationCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

    }
}
