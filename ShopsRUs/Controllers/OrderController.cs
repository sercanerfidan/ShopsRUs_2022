using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopsRUs.Domain.Orders;
using ShopsRUs.Domain.Exceptions;
using ShopsRUs.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Controllers
{
    public class OrderController : BaseController
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator mediator;

        public OrderController(ILogger<OrderController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpGet("GetOrders/{CustomerId}")]
        public async Task<ActionResult<Order>> GetOrders(int customerId)
        {
            var result = await mediator.Send(new GetOrdersRequest(customerId));
            return Ok(result);
        }

        [HttpPost("CreateOrder")]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] CreateOrderRequest request) {

            if (request is null)
                throw new CustomException("Body is empty");


            var order = await mediator.Send(request);
            return Ok(order);
        }
    }
}
