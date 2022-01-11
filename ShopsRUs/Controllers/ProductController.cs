using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopsRUs.Domain.Exceptions;
using ShopsRUs.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMediator mediator;

        public ProductController(ILogger<ProductController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<Product>> GetProducts()
        {
            var result = await mediator.Send(new GetProductsRequest());
            return Ok(result);
        }


        [HttpGet("GetProductById/{Id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var result = await mediator.Send(new GetProductByIdRequest(id));
            return Ok(result);
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<Product>> AddProduct([FromBody] AddProductRequest request)
        {

            if (request is null)
                throw new CustomException("Body is empty");

            var product = await mediator.Send(request);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);

        }
    }
}
