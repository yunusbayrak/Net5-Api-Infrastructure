using Hepsiorada.Domain.Products;
using Hepsiorada.Domain.Users;
using Hepsiorada.Domain.Users.Lists;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Base
{
    public interface IUnitOfWork
    {
        public IUserListRepository UserListRepository { get; }
        public IUserRepository UserRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IProductMongoRepository ProductMongoRepository { get; }

        Task SaveChangesAsync();
    }
}
