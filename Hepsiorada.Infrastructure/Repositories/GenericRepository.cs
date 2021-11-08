using Dapper;
using Hepsiorada.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hepsiorada.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : IEntity
    {
        protected readonly DbContext _dbContext;
        public readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext can not be null");

            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            if (entity == null)
                throw new ApplicationException(typeof(T).Name + " not found");

            await Delete(entity);
        }
        public Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }
        public virtual Task Update(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
