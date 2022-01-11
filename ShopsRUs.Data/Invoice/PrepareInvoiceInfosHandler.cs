using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Data.Discounts;
using ShopsRUs.Domain.Customers;
using ShopsRUs.Domain.Discounts;
using ShopsRUs.Domain.Enums;
using ShopsRUs.Domain.Exceptions;
using ShopsRUs.Domain.Invoices;
using ShopsRUs.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopsRUs.Data.Invoices
{
    public class PrepareInvoiceInfosHandler : IRequestHandler<PrepareInvoiceInfosRequest, Invoice>
    {

        private readonly ApplicationDbContext context;
        private readonly IMediator mediator;
        private readonly IDiscountService discountSerivce;

        public PrepareInvoiceInfosHandler(ApplicationDbContext context, IMediator mediator, IDiscountService discountSerivce)
        {
            this.context = context;
            this.mediator = mediator;
            this.discountSerivce = discountSerivce;
        }

        public Task<Invoice> Handle(PrepareInvoiceInfosRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return PrepareInvoiceInfos();

            async Task<Invoice> PrepareInvoiceInfos()
            {
                var customer = await mediator.Send(new GetCustomerRequest(request.CustomerId));
                var product = await mediator.Send(new GetProductByIdRequest(request.ProductId));
                var discounts = await mediator.Send(new GetDiscountsRequest());

                var discountAmount = discountSerivce.DecideDiscountAmount(customer, product, discounts);
                decimal netAmount = product.Price - discountAmount;

                return new Invoice(product.Price, discountAmount, netAmount, customer.Id, product.Id, request.OrderId);

            }
        }
    }

}
