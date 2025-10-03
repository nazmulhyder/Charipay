﻿using Charipay.Application.Commands.Charities;
using Charipay.Application.Queries.Charities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charipay.API.Controllers
{
    [ApiController] // ✅ Tells ASP.NET Core this is an API controller
    [Route("api/[controller]")] // ✅ Base route will be api/users
    public class CharityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;

        public CharityController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;

        }

        [HttpPost("CreateChairy")]
        public async Task<IActionResult> CreateChairy([FromBody] CreateCharityCommand request, CancellationToken token)
        {
            var result = await _mediator.Send(request);

            if (result.Message.Contains("exists"))
                return BadRequest(result);

            return Ok(result);
            
        }

        [HttpGet("GetAllCharity")]
        public async Task<IActionResult> GetAllCharity(CancellationToken token)
        {
            var result = await _mediator.Send(new GetAllCharityQuery());

            return Ok(result);
        }
    }
}
