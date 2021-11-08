using Hepsiorada.Domain.Base;
using Hepsiorada.Domain.Products;
using Hepsiorada.Domain.Users;
using Hepsiorada.Domain.Users.Lists;
using Hepsiorada.Infrastructure.Database.Contexts;
using Hepsiorada.Infrastructure.Repositories;
using Hepsiorada.Infrastructure.Repositories.Products;
using Hepsiorada.Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hepsiorada.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<HepsiOradaDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"), b => b.MigrationsAssembly("Hepsiorada.Infrastructure"));
            }, ServiceLifetime.Scoped);
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<IUserListRepository, UserListRepository>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IProductRepository, ProductRepository>();
            serviceCollection.AddTransient<IProductMongoRepository, ProductMongoRepository>();

            serviceCollection.AddStackExchangeRedisCache(options => options.Configuration = configuration.GetSection("Redis")["Configuration"]);
        }
    }
}
