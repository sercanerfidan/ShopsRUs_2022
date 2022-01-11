using MediatR;
using ShopsRUs.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using ShopsRUs.Domain.Customers;
using System.Threading;
using ShopsRUs.Domain.Enums;
using ShopsRUs.Domain.Invoices;

namespace ShopsRUs.Test.Api.Controllers
{
    public class InvoiceControllerTest
    {
        [Fact]
        public void InheritFromBaseController()
        {
            Assert.True(typeof(InvoiceController).IsAssignableTo(typeof(BaseController)));
        }

        [Fact]
        public async Task SendGetInvoiceByOrderIdRequest()
        {
            var mediator = new Mock<IMediator>();
            var logger = new Mock<ILogger<InvoiceController>>();
            var controller = new InvoiceController(logger.Object, mediator.Object);
            var orderId = new Random().Next();

            await controller.GetInvoiceByOrderId(orderId);

            mediator.Verify(x => x.Send(new GetInvoiceByOrderIdRequest(orderId), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task SendGetInvoiceByCustomerIdRequest()
        {
            var mediator = new Mock<IMediator>();
            var logger = new Mock<ILogger<InvoiceController>>();
            var controller = new InvoiceController(logger.Object, mediator.Object);
            var orderId = new Random().Next();

            var getInvoiceRequest = new GetInvoiceByCustomerIdRequest(orderId);

          
            await controller.GetInvoiceByCustomerId(getInvoiceRequest);

            mediator.Verify(x => x.Send(getInvoiceRequest, CancellationToken.None), Times.Once);
        }
    }
}
