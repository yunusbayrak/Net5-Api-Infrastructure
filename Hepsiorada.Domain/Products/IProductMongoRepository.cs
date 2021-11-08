using Hepsiorada.Domain.Products.MongoEntites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Products
{
    public interface IProductMongoRepository
    {
        Task Add(List<TopListedProduct> products);
        Task Add(List<UserTopListedProduct> products);
        Task<List<UserTopListedProduct>> GetAllUserTopListedProducts();
        Task<List<TopListedProduct>> GetAllTopListedProducts();
        Task RemoveTopListedProducts();
        Task RemoveUserTopListedProducts();
    }
}
