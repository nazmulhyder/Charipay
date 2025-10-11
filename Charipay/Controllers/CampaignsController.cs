using Charipay.Application.Commands.Campaigns;
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
    }
}
