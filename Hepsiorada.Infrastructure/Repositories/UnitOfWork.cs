using Hepsiorada.Domain.Base;
using Hepsiorada.Domain.Products;
using Hepsiorada.Domain.Users;
using Hepsiorada.Domain.Users.Lists;
using Hepsiorada.Infrastructure.Database.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Hepsiorada.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IServiceProvider _serviceProvider;
        private readonly HepsiOradaDbContext _context;
        public UnitOfWork(IServiceProvider serviceProvider, HepsiOradaDbContext context)
        {
            _serviceProvider = serviceProvider;
            _context = context;
        }

        public IUserRepository UserRepository => _serviceProvider.GetService<IUserRepository>();
        public IProductRepository ProductRepository => _serviceProvider.GetService<IProductRepository>();
        public IUserListRepository UserListRepository => _serviceProvider.GetService<IUserListRepository>();
        public IProductMongoRepository ProductMongoRepository => _serviceProvider.GetService<IProductMongoRepository>();

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
