using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopsRUs.Domain.Exceptions;
using ShopsRUs.Domain.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Controllers
{
    public class InvoiceController : BaseController
    {
        private readonly ILogger<InvoiceController> _logger;
        private readonly IMediator mediator;

        public InvoiceController(ILogger<InvoiceController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpGet("GetInvoiceByOrderId/{orderId}")]
        public async Task<ActionResult<Invoice>> GetInvoiceByOrderId(int orderId)
        {
            var result = await mediator.Send(new GetInvoiceByOrderIdRequest(orderId));
            return Ok(result);
        }

        [HttpGet("GetInvoiceByCustomerId/{customerId}")]
        public async Task<ActionResult<Invoice>> GetInvoiceByCustomerId([FromBody] GetInvoiceByCustomerIdRequest request)
        {

            if (request is null)
                throw new CustomException("Body is empty");

            var invoices = await mediator.Send(request);
            return Ok(invoices);
        }
    }
}
