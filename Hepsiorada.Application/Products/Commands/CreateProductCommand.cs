using MediatR;
using System;

namespace Hepsiorada.Application.Products.Commands
{
    public class CreateProductCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string Brand { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
    }
}
