using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Products.MongoEntites
{
    public class ProductMongo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string Brand { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
    }
}
