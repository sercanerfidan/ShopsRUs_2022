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
using ShopsRUs.Domain.Products;

namespace ShopsRUs.Test.Api.Controllers
{
    public class ProductControllerTest
    {
        [Fact]
        public void InheritFromBaseController()
        {
            Assert.True(typeof(ProductController).IsAssignableTo(typeof(BaseController)));
        }

        [Fact]
        public async Task SendGetProductsRequest()
        {
            var mediator = new Mock<IMediator>();
            var logger = new Mock<ILogger<ProductController>>();
            var controller = new ProductController(logger.Object, mediator.Object);

            await controller.GetProducts();

            mediator.Verify(x => x.Send(new GetProductsRequest(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task SendGetProductByIdRequest()
        {
            var mediator = new Mock<IMediator>();
            var logger = new Mock<ILogger<ProductController>>();
            var controller = new ProductController(logger.Object, mediator.Object);
            var productId = new Random().Next();

            var request = new GetProductByIdRequest(productId);

           
            await controller.GetProductById(productId);

            mediator.Verify(x => x.Send(request, CancellationToken.None), Times.Once);
        }
    }
}
