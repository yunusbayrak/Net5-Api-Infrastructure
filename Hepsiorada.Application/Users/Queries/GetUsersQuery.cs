using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Application.Users.Queries
{
    public class GetUsersQuery: IRequest<IEnumerable<UserDto>>
    {
    }
}
