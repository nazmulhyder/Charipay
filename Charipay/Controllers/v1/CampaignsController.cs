using Asp.Versioning;
using Charipay.Application.Commands.Campaigns;
using Charipay.Application.Queries.Campaigns;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charipay.API.Controllers.v1
{
    
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    /// <summary>
    /// 
    /// </summary>
    public class CampaignsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CampaignsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateCampaign")]
        public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignCommand request, CancellationToken token)
        {
            var result = await _mediator.Send(request);

            if (result.Message.Contains("exists"))
            return BadRequest(result);

            return Ok(result);  

        }

        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateCampaign")]
        public async Task<IActionResult> UpdateCampaign([FromBody] UpdateCampaignCommand request, CancellationToken token)
        {
            var result = await _mediator.Send(request);

            if (result.Message.Contains("exists"))
                return BadRequest(result);

            return Ok(result);

        }

        [HttpGet("Public/AllCampaigns")]
        public async Task<IActionResult> GetPagedCampaigns([FromQuery] GetAllPagedCampaignsQuery query, CancellationToken token)
        {
            var result = await _mediator.Send(query);

            if (result.Message.Contains("exists"))
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("Admin/AllCampaigns")]
        public async Task<IActionResult> GetAllCampaigns([FromQuery] GetAllCampaignsAdminQuery query, CancellationToken token)
        {
            var result = await _mediator.Send(query);

            if (result.Message.Contains("exists"))
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteCampaigns")]
        public async Task<IActionResult> DeleteCampaigns([FromQuery] DeleteCampaignCommand query, CancellationToken token)
        {
            var result = await _mediator.Send(query);

            if (result.Message.Contains("exists"))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] GetByIdCampaignQuery query, CancellationToken token)
        {
            var result = await _mediator.Send(query);

            if (result.Message.Contains("exists"))
                return BadRequest(result);

            return Ok(result);
        }
    }
}
