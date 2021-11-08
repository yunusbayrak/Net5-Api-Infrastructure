using Hepsiorada.Domain.Products.MongoEntites;
using MediatR;
using System.Collections.Generic;

namespace Hepsiorada.Application.Products.Queries
{
    public class GetUserTopListedProductsQuery : IRequest<IEnumerable<UserTopListedProduct>>
    {
    }
}
