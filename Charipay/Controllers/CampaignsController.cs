using Charipay.Application.Commands.Campaigns;
using Charipay.Application.Queries.Campaigns;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charipay.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPost("CreateCampaign")]
        public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignCommand request, CancellationToken token)
        {
            var result = await _mediator.Send(request);

            if (result.Message.Contains("exists"))
            return BadRequest(result);

            return Ok(result);  

        }

        [HttpPost("UpdateCampaign")]
        public async Task<IActionResult> UpdateCampaign([FromBody] UpdateCampaignCommand request, CancellationToken token)
        {
            var result = await _mediator.Send(request);

            if (result.Message.Contains("exists"))
                return BadRequest(result);

            return Ok(result);

        }

        [HttpGet("GetAllCampaigns")]
        public async Task<IActionResult> GetAllPagedCampaigns([FromQuery] GetAllPagedCampaignsQuery query, CancellationToken token)
        {
            var result = await _mediator.Send(query);

            if (result.Message.Contains("exists"))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("DeleteCampains")]
        public async Task<IActionResult> DeleteCampains([FromQuery] DeleteCampaignCommand query, CancellationToken token)
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
