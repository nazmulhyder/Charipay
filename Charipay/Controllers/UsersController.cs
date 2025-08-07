using AutoMapper;
using Charipay.Application.Commands;
using Charipay.Domain.Entities;
using Charipay.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charipay.API.Controllers
{

    [ApiController] // ✅ Tells ASP.NET Core this is an API controller
    [Route("api/[controller]")] // ✅ Base route will be api/users
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;       
        private readonly ILogger<UsersController> _logger;

        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
          
        }

        // POST: api/users
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] RegisterUserCommand request)
        {
           var result = await _mediator.Send(request);
           return Ok(result);
          
        }

  
    }
}
