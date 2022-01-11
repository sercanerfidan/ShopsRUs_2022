using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Domain.Customers;
using ShopsRUs.Domain.Exceptions;
using ShopsRUs.Domain.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopsRUs.Data.Invoices
{
    public class GetInvoiceByOrderIdHandler : IRequestHandler<GetInvoiceByOrderIdRequest, Invoice>
    {

        private readonly ApplicationDbContext context;

        public GetInvoiceByOrderIdHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<Invoice> Handle(GetInvoiceByOrderIdRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return GetInvoice();

            async Task<Invoice> GetInvoice()
            {
                var invoice = await context.Invoices.Where(x => x.OrderId== request.OrderId).FirstOrDefaultAsync(); 

                if (invoice is null)
                    throw new CustomException(HttpStatusCode.NotFound, $"Invoice with the orderId {request.OrderId} cannot be found!");

                return invoice;
            }
        }
    }

}
