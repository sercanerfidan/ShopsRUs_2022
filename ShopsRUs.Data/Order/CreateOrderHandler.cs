using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Domain.Customers;
using ShopsRUs.Domain.Enums;
using ShopsRUs.Domain.Exceptions;
using ShopsRUs.Domain.Invoices;
using ShopsRUs.Domain.Orders;
using ShopsRUs.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopsRUs.Data.Products
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, Order>
    {

        private readonly ApplicationDbContext context;
        private readonly IMediator mediator;
        public CreateOrderHandler(ApplicationDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public Task<Order> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return CreateOrder();

            async Task<Order> CreateOrder()
            {
                var order = new Order(request.CustomerId, request.ProductId);
                await ValidateOrder(request);

                await context.Orders.AddAsync(order, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                var invoice = await mediator.Send(new PrepareInvoiceInfosRequest(request.CustomerId, request.ProductId, order.Id));

                await mediator.Send(new CreateInvoiceRequest(invoice.Amount, invoice.Discount, invoice.NetAmount, invoice.CustomerId, invoice.ProductId, invoice.OrderId));
                
                return order;
            }
        }

        public async Task ValidateOrder(CreateOrderRequest request) {

            await mediator.Send(new GetCustomerRequest(request.CustomerId));
            await mediator.Send(new GetProductByIdRequest(request.ProductId));

        }
    }

}
