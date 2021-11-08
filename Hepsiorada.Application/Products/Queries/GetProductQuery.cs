using MediatR;
using System;
using System.Collections.Generic;

namespace Hepsiorada.Application.Products.Queries
{
    public class GetProductQuery : IRequest<ProductDto>
    {
        public Guid Id { get; set; }
        public GetProductQuery(Guid id)
        {
            Id = id;
        }
    }
}
