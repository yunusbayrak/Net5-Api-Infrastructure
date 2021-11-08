using Dapper;
using Hepsiorada.Domain.Users.Lists;
using Hepsiorada.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Infrastructure.Repositories.Users
{
    public class UserListRepository : GenericRepository<UserList>, IUserListRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _tableName;
        public UserListRepository(HepsiOradaDbContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _configuration = configuration;
            _tableName = "\"ListItems\"";
        }

        public async Task AddToList(ListItem entity)
        {
            await _dbContext.Set<ListItem>().AddAsync(entity);
        }

        public async Task AddToList(IEnumerable<ListItem> list)
        {
            await _dbContext.Set<ListItem>().AddRangeAsync(list);
        }

        public async Task<IEnumerable<ListItem>> GetListItemsByListId(Guid id)
        {
            return await _dbContext.Set<ListItem>().Where(x => x.Id == id).ToListAsync();
        }

        public Task RemoveFromList(ListItem entity)
        {
            _dbContext.Set<ListItem>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task RemoveFromList(IEnumerable<Guid> ids)
        {
            var Ids = ids.Select(x => ("\"" + x + "\"").ToString()).ToArray();
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQL"));
            int affectedRows = await connection.ExecuteAsync($"DELETE FROM {_tableName} WHERE \"Id\" IN(@Ids)", new { Ids });
        }

        public async Task RemoveFromList(Guid id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQL"));
            int affectedRows = await connection.ExecuteAsync($"DELETE FROM {_tableName} WHERE \"Id\" = @id", new { id });
        }

        public async Task<IEnumerable<ListItemCountDto>> GetMostListedProducts(int count)
        {
            var res = await _dbContext.Set<ListItem>().GroupBy(x => x.ProductId)
                                .Select(m => new ListItemCountDto { ProductId = m.Key, Count = m.Count() })
                                .OrderByDescending(x => x.Count).Take(count).ToListAsync();
            return res;
        }

        public async Task<IEnumerable<UserListItemCountDto>> GetUsersMostListedProducts(int count)
        {
            var res = await _dbContext.Set<ListItem>().Include(x => x.UserList).GroupBy(x => new { x.UserList.UserId, x.ProductId })
                                .Select(m => new UserListItemCountDto { UserId = m.Key.UserId, ProductId = m.Key.ProductId, Count = m.Count() })
                                .OrderByDescending(x => x.Count).Take(count).ToListAsync();
            return res;
        }
    }
}
