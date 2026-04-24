using Asp.Versioning;
using Charipay.Application.Commands.Campaigns;
using Charipay.Application.Commands.Donations;
using Charipay.Application.Queries.Donor;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charipay.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles ="Donor")]
    /// <summary>
    /// 
    /// </summary>
    public class DonationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DonationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateDonation")]
        public async Task<IActionResult> CreateDonation([FromBody] CreateDonationCommand request, CancellationToken token)
        {
            var result = await _mediator.Send(request);

            if (result.Message.Contains("exists"))
                return BadRequest(result);

            return Ok(result);

        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> dashboard([FromQuery] GetDonorDashboardQuery request, CancellationToken token)
        {
            var result = await _mediator.Send(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);

        }

        [HttpGet("donations")]
        public async Task<IActionResult> donations([FromQuery] GetPagedDonorDonationsQuery request, CancellationToken token)
        {
            var result = await _mediator.Send(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);

        }

        [HttpGet("donationById")]
        public async Task<IActionResult> donationById([FromQuery] GetDonationByIdQuery request, CancellationToken token)
        {
            var result = await _mediator.Send(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);

        }
    }
}
