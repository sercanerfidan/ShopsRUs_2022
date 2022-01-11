using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopsRUs.Domain.Discounts;
using ShopsRUs.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Controllers
{
    public class DiscountController : BaseController
    {

        private readonly ILogger<DiscountController> _logger;
        private readonly IMediator mediator;

        public DiscountController(ILogger<DiscountController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpGet("GetDiscounts")]
        public async Task<ActionResult<Discount>> GetDiscounts()
        {
            var result = await mediator.Send(new GetDiscountsRequest());
            return Ok(result);
        }


        [HttpPost("AddDiscount")]
        public async Task<ActionResult<Discount>> AddDiscount([FromBody] AddDiscountRequest request)
        {

            if (request is null)
                throw new CustomException("Body is empty");

            var discount = await mediator.Send(request);
            return Ok(discount);
        }
    }
}
