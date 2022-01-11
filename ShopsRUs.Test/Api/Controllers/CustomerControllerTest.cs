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

namespace ShopsRUs.Test.Api.Controllers
{
    public class CustomerControllertest
    {
        [Fact]
        public void InheritFromBaseController()
        {
            Assert.True(typeof(CustomerController).IsAssignableTo(typeof(BaseController)));
        }

        [Fact]
        public async Task SendGetCustomerRequest()
        {
            var mediator = new Mock<IMediator>();
            var logger = new Mock<ILogger<CustomerController>>();
            var controller = new CustomerController(logger.Object, mediator.Object);
            var customerId = new Random().Next();

            await controller.GetCustomer(customerId);

            mediator.Verify(x => x.Send(new GetCustomerRequest(customerId), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task SendCreateCustomerRequest()
        {
            var mediator = new Mock<IMediator>();
            var logger = new Mock<ILogger<CustomerController>>();
            var controller = new CustomerController(logger.Object, mediator.Object);

            var createCustomerRequest = new CreateCustomerRequest(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            mediator.Setup(x => x.Send(createCustomerRequest, CancellationToken.None))
            .Returns(Task.FromResult<Customer>(new Customer(string.Empty, string.Empty, CustomerType.Affilate) {Id = new Random().Next() } ));

          
            await controller.CreateCustomer(createCustomerRequest);

            mediator.Verify(x => x.Send(createCustomerRequest, CancellationToken.None), Times.Once);
        }
    }
}
