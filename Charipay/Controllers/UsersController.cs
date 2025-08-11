using AutoMapper;
using Charipay.Application.Commands;
using Charipay.Application.Commands.Users;
using Charipay.Application.Queries.Users;
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
        public async Task<IActionResult> Create([FromBody] CreateUserCommand request)
        {
           var result = await _mediator.Send(request);
           return Ok(result);
          
        }


        // GET: api/users
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByUserId(Guid id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            if(result ==  null) return NotFound();

            return Ok(result);

        }


    }
}
