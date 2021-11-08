using Hepsiorada.Api.Helpers;
using Hepsiorada.Application.Users;
using Hepsiorada.Application.Users.Commands;
using Hepsiorada.Application.Users.Queries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;


namespace Hepsiorada.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            return Ok(ApiResponseHelper.SuccessResponse(users));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _mediator.Send(new GetUserQuery(id));
            return Ok(ApiResponseHelper.SuccessResponse(user));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDto user)
        {
            var cmd = user.Adapt<CreateUserCommand>();
            await _mediator.Send(cmd);
            return Ok(ApiResponseHelper.SuccessResponse());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UserDto user)
        {
            var cmd = user.Adapt<UpdateUserCommand>();
            cmd.Id = id;
            await _mediator.Send(cmd);
            return Ok(ApiResponseHelper.SuccessResponse());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return Ok(ApiResponseHelper.SuccessResponse());
        }        
    }
}
