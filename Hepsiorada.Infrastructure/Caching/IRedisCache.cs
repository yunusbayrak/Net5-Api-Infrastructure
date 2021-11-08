using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hepsiorada.Infrastructure.Caching
{
    public interface IRedisCache<T>
    {
        Task<T> GetAsync(string key, CancellationToken token = default);
        void Refresh(string key);
        Task RefreshAsync(string key, CancellationToken token = default);

        void Remove(string key);

        Task RemoveAsync(string key, CancellationToken token = default);

        void Set(string key, T value, DistributedCacheEntryOptions options);

        Task SetAsync(string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default);
    }
}
