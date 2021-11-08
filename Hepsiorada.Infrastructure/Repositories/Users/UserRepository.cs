using Hepsiorada.Domain.Users;
using Hepsiorada.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiorada.Infrastructure.Repositories.Users
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(HepsiOradaDbContext context) : base(context)
        {

        }

        public async Task<User> GetByEMail(string email)
        {
            return await _dbSet.Where(x => x.Email == email).SingleOrDefaultAsync();
        }

        public override async Task Update(User entity)
        {
            var user = await GetById(entity.Id);
            if (user == null)
            {
                throw new ApplicationException("User not found!");
            }
            user.Name = entity.Name;
            user.Surname = entity.Surname;
            user.Address = entity.Address;
            user.PhoneNumber = entity.PhoneNumber;
            user.Email = entity.Email;
            _dbSet.Update(user);
        }
    }
}
