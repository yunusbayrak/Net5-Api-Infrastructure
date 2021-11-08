using MediatR;
using System.Collections.Generic;

namespace Hepsiorada.Application.Products.Queries
{
    public class GetProductsQuery: IRequest<IEnumerable<ProductDto>>
    {
    }
}
