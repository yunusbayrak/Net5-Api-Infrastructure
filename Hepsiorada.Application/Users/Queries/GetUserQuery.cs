using MediatR;
using System;

namespace Hepsiorada.Application.Users.Queries
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public Guid Id { get; set; }
        public GetUserQuery(Guid id)
        {
            Id = id;
        }
    }
}
