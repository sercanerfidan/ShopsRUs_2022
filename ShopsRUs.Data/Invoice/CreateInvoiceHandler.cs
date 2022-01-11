using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Domain.Customers;
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
    public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceRequest, Invoice>
    {

        private readonly ApplicationDbContext context;

        public CreateInvoiceHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<Invoice> Handle(CreateInvoiceRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return CreateInvoice();

            async Task<Invoice> CreateInvoice()
            {
                var invoice = new Invoice(request.Amount, request.Discount, request.NetAmount, request.CustomerId, request.ProductId, request.OrderId);
                 
                await context.Invoices.AddAsync(invoice, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return invoice;
            }
        }
    }

}
