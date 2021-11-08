using Hepsiorada.Api.Helpers;
using Hepsiorada.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiorada.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ReportsController> _logger;
        public ReportsController(IMediator mediator, ILogger<ReportsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("toplistedproducts")]
        public async Task<IActionResult> GetTopListedProducts()
        {
            var products = await _mediator.Send(new GetTopListedProductsQuery());
            return Ok(ApiResponseHelper.SuccessResponse(products));
        }

        [HttpGet("usertoplistedproducts")]
        public async Task<IActionResult> GetUserTopListedProducts()
        {
            var products = await _mediator.Send(new GetUserTopListedProductsQuery());
            return Ok(ApiResponseHelper.SuccessResponse(products));
        }
    }
}
