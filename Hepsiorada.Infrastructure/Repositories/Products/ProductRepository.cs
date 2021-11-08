using Dapper;
using Hepsiorada.Domain.Products;
using Hepsiorada.Domain.Products.ProductMeta;
using Hepsiorada.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hepsiorada.Infrastructure.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        protected readonly DbContext _dbContext;
        public readonly DbSet<Product> _dbSet;
        private readonly IConfiguration _configuration;
        private readonly string _tableName;
        public ProductRepository(HepsiOradaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Product>();
            _configuration = configuration;
            _tableName = $"\"Products\"";
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQL"));
            var list = await connection.QueryAsync<Product>($"SELECT * FROM {_tableName}");
            return list;
        }
        public async Task<Product> GetById(Guid id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQL"));
            return await connection.QueryFirstOrDefaultAsync<Product>($"SELECT * FROM {_tableName} WHERE \"Id\" = @Id", new { Id = id });
        }
        public async Task Add(Product entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public Task Update(Product entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }
        public Task Delete(Product entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }
        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            if (entity == null)
                return;

            await Delete(entity);
        }

        public async Task<IEnumerable<Product>> Get(Expression<Func<Product, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task CreateCategory(Category category)
        {
            await _dbContext.Set<Category>().AddAsync(category);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQL"));
            return await connection.QueryAsync<Category>("SELECT * FROM \"Categories\"");
        }
    }
}
