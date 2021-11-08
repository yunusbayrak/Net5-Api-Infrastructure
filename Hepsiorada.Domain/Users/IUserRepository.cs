using Hepsiorada.Domain.Base;
using Hepsiorada.Domain.Users.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEMail(string email);
    }
}
