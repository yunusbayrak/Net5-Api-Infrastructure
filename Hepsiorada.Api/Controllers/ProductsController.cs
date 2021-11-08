using Hepsiorada.Api.Helpers;
using Hepsiorada.Application.Products;
using Hepsiorada.Application.Products.Commands;
using Hepsiorada.Application.Products.Queries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Hepsiorada.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(ApiResponseHelper.SuccessResponse(products));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _mediator.Send(new GetProductQuery(id));
            return Ok(ApiResponseHelper.SuccessResponse(user));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto user)
        {
            var cmd = user.Adapt<CreateProductCommand>();
            await _mediator.Send(cmd);
            return Ok(ApiResponseHelper.SuccessResponse());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ProductDto product)
        {
            var cmd = product.Adapt<UpdateProductCommand>();
            cmd.Id = id;
            await _mediator.Send(cmd);
            return Ok(ApiResponseHelper.SuccessResponse());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return Ok(ApiResponseHelper.SuccessResponse());
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var user = await _mediator.Send(new GetCategoriesQuery());
            return Ok(ApiResponseHelper.SuccessResponse(user));
        }

        [HttpPost("categories")]
        public async Task<IActionResult> PostCategories([FromBody] CategoryDto category)
        {
            var cmd = category.Adapt<CreateCategoryCommand>();
            await _mediator.Send(cmd);
            return Ok(ApiResponseHelper.SuccessResponse());
        }
    }
}
