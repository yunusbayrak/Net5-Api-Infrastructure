using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Application.UserLists.Commands
{
    public class DeleteUserListCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DeleteUserListCommand(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
