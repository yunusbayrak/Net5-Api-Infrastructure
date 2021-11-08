using Hepsiorada.Domain.Base;
using Hepsiorada.Domain.Products.ProductMeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task CreateCategory(Category category);
        Task<IEnumerable<Category>> GetCategories();
    }
}
