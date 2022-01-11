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
using ShopsRUs.Domain.Discounts;

namespace ShopsRUs.Test.Api.Controllers
{
    public class DiscountControllerTest
    {
        [Fact]
        public void InheritFromBaseController()
        {
            Assert.True(typeof(DiscountController).IsAssignableTo(typeof(BaseController)));
        }

        [Fact]
        public async Task SendGetDiscountsRequest()
        {
            var mediator = new Mock<IMediator>();
            var logger = new Mock<ILogger<DiscountController>>();
            var controller = new DiscountController(logger.Object, mediator.Object);

            await controller.GetDiscounts();

            mediator.Verify(x => x.Send(new GetDiscountsRequest(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task SendAddDiscountRequest()
        {
            var mediator = new Mock<IMediator>();
            var logger = new Mock<ILogger<DiscountController>>();
            var controller = new DiscountController(logger.Object, mediator.Object);

            var discountRequest = new AddDiscountRequest(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(),
                new Random().Next(5,80), Guid.NewGuid().ToString(), new Random().Next(50,500), new Random().Next(50, 100));

            
            await controller.AddDiscount(discountRequest);

            mediator.Verify(x => x.Send(discountRequest, CancellationToken.None), Times.Once);
        }
    }
}
