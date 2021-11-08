using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Application.Users.Queries
{
    public class GetUserByEmailAndPasswordQuery : IRequest<UserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public GetUserByEmailAndPasswordQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
