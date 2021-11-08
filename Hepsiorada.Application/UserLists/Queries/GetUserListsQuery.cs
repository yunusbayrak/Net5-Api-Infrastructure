using MediatR;
using System;
using System.Collections.Generic;

namespace Hepsiorada.Application.UserLists.Queries
{
    public class GetUserListsQuery : IRequest<IEnumerable<UserListDto>>
    {
        public Guid UserId { get; set; }
        public GetUserListsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
