using MediatR;
using System;

namespace Hepsiorada.Application.Products.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
