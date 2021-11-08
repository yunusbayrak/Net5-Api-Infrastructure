using Hepsiorada.Api.Helpers;
using Hepsiorada.Application.UserLists;
using Hepsiorada.Application.UserLists.Commands;
using Hepsiorada.Application.UserLists.Queries;
using Mapster;
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
    public class ListsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ListsController> _logger;
        public ListsController(IMediator mediator, ILogger<ListsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get([FromRoute] Guid userId)
        {
            var lists = await _mediator.Send(new GetUserListsQuery(userId));
            return Ok(ApiResponseHelper.SuccessResponse(lists));
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> Post([FromRoute] Guid userId, [FromBody] UserListDto userList)
        {
            var cmd = userList.Adapt<CreateUserListCommand>();
            cmd.UserId = userId;
            await _mediator.Send(cmd);
            return Ok(ApiResponseHelper.SuccessResponse());
        }

        [HttpPut("{listId}/user/{userId}")]
        public async Task<IActionResult> Put([FromRoute] Guid userId, [FromRoute] Guid listId, [FromBody] UserListDto userList)
        {
            var cmd = userList.Adapt<UpdateUserListCommand>();
            cmd.UserId = userId;
            cmd.Id = listId;
            await _mediator.Send(cmd);
            return Ok(ApiResponseHelper.SuccessResponse());
        }
        [HttpDelete("{listId}/user/{userId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid userId, [FromRoute] Guid listId)
        {
            await _mediator.Send(new DeleteUserListCommand(listId, userId));
            return Ok(ApiResponseHelper.SuccessResponse());
        }

        [HttpPost("{listId}/user/{userId}/Items")]
        public async Task<IActionResult> AddProductsToList([FromRoute] Guid userId, [FromRoute] Guid listId, [FromBody] List<Guid> productIds)
        {
            await _mediator.Send(new CreateListItemsCommand(listId, userId, productIds));
            return Ok(ApiResponseHelper.SuccessResponse());
        }

        [HttpPost("{listId}/user/{userId}/Items/Remove")]
        public async Task<IActionResult> RemoveProductsFromList([FromRoute] Guid userId, [FromRoute] Guid listId, [FromBody] List<Guid> productIds)
        {
            await _mediator.Send(new DeleteListItemsCommand(listId, userId, productIds));
            return Ok(ApiResponseHelper.SuccessResponse());
        }
    }
}
