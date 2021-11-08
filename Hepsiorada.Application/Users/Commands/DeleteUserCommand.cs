using MediatR;
using System;

namespace Hepsiorada.Application.Users.Commands
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
