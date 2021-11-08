using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Products.MongoEntites
{
    public class UserTopListedProduct:TopListedProduct
    {
        public UserMongo User { get; set; }
    }
}
