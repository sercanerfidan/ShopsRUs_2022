using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopsRUs.Domain.Customers;
using ShopsRUs.Domain.Exceptions;
using ShopsRUs.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IMediator mediator;

        public CustomerController(ILogger<CustomerController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpGet("GetCustomer/{Id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var result = await mediator.Send(new GetCustomerRequest(id));
            return Ok(result);
        }

        [HttpPost("CreateCustomer")]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] CreateCustomerRequest request) {

            if (request is null)
                throw new CustomException("Body is empty");

            var customer = await mediator.Send(request);
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }


        [HttpGet]
        public string  Get()
        {
            return "Welcome to ShopsRUs";
        }
    }
}
