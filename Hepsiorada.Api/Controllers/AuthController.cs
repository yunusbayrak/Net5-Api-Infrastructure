using Hepsiorada.Api.Helpers;
using Hepsiorada.Application.Users;
using Hepsiorada.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiorada.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _mediator.Send(new GetUserByEmailAndPasswordQuery(loginDto.username, loginDto.password));
            user.Password = null;
            //Generate Token
            var authToken = TokenGenerator.GenerateToken(user.Id.ToString());
            var authResponse = new { User = user, Token = authToken.Token };

            var successResponse = ApiResponseHelper.SuccessResponse(authResponse);
            return Ok(successResponse);
        }
    }
}
