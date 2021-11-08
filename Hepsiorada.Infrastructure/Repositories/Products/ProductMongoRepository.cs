using Hepsiorada.Domain.Base;
using Hepsiorada.Domain.Products;
using Hepsiorada.Domain.Products.MongoEntites;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Infrastructure.Repositories.Products
{
    public class ProductMongoRepository : IProductMongoRepository
    {
        private readonly IConfiguration _configuration;
        private IMongoCollection<TopListedProduct> _topListedproducts;
        private IMongoCollection<UserTopListedProduct> _userTopListedproducts;

        public ProductMongoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient(_configuration.GetConnectionString("HepsiOradaMongoDb"));
            var database = client.GetDatabase("Hepsiorada");

            _topListedproducts = database.GetCollection<TopListedProduct>("TopListedProducts");
            _userTopListedproducts = database.GetCollection<UserTopListedProduct>("UserTopListedProducts");
        }
        public async Task Add(List<TopListedProduct> products)
        {
            await _topListedproducts.InsertManyAsync(products);
        }

        public async Task Add(List<UserTopListedProduct> products)
        {
            await _userTopListedproducts.InsertManyAsync(products);
        }

        public async Task<List<TopListedProduct>> GetAllTopListedProducts()
        {
            return (await _topListedproducts.FindAsync(m => true)).ToList();
        }
        public async Task<List<UserTopListedProduct>> GetAllUserTopListedProducts()
        {
            return (await _userTopListedproducts.FindAsync(m => true)).ToList();
        }

        public async Task RemoveTopListedProducts()
        {
            await _topListedproducts.DeleteManyAsync(m => true);
        }
        public async Task RemoveUserTopListedProducts()
        {
            await _userTopListedproducts.DeleteManyAsync(m => true);
        }
    }
}
