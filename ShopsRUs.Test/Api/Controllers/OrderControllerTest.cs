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
using ShopsRUs.Domain.Orders;
using ShopsRUs.Domain.Products;

namespace ShopsRUs.Test.Api.Controllers
{
    public class OrderControllerTest
    {
        [Fact]
        public void InheritFromBaseController()
        {
            Assert.True(typeof(OrderController).IsAssignableTo(typeof(BaseController)));
        }

        [Fact]
        public async Task SendGetOrdersRequest()
        {
            var mediator = new Mock<IMediator>();
            var logger = new Mock<ILogger<OrderController>>();
            var controller = new OrderController(logger.Object, mediator.Object);
            var customerId = new Random().Next();

            await controller.GetOrders(customerId);

            mediator.Verify(x => x.Send(new GetOrdersRequest(customerId), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task SendCreateOrderRequest()
        {
            var mediator = new Mock<IMediator>();
            var logger = new Mock<ILogger<OrderController>>();
            var controller = new OrderController(logger.Object, mediator.Object);
            var customerId = new Random().Next();
            var orderId = new Random().Next();

            var createCustomerRequest = new CreateOrderRequest(customerId, orderId);

             
            await controller.CreateOrder(createCustomerRequest);

            mediator.Verify(x => x.Send(createCustomerRequest, CancellationToken.None), Times.Once);
        }
    }
}
