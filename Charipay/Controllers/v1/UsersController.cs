using Asp.Versioning;
using Charipay.Application.Commands.Users;
using Charipay.Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charipay.API.Controllers.v1
{

    [ApiController] // ✅ Tells ASP.NET Core this is an API controller
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // ✅ Base route will be api/users
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;       
        private readonly ILogger<UsersController> _logger;

        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
          
        }


        // GET: api/users/GetAllUser
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _mediator.Send(new GetAllUserQuery());

            if (result == null) return NotFound();

            return Ok(result);

        }

        // GET: api/users/GetByUserId/id
        [HttpGet("GetByUserId/{id:guid}")]
        public async Task<IActionResult> GetByUserId(Guid id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            if(result ==  null) return NotFound();

            return Ok(result);

        }

        // PUT: api/users/update
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand user)
        {
            var result = await _mediator.Send(user);

            if (result == null) return NotFound();

            return Ok(result);

        }

        // PUT: api/users/update
        [HttpDelete("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id));

            if (result == null) return NotFound();

            return Ok(result);
        }



    }
}
