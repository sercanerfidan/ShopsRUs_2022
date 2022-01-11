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
    public class GetInvoiceByCustomerIdHandler : IRequestHandler<GetInvoiceByCustomerIdRequest, IEnumerable<Invoice>>
    {

        private readonly ApplicationDbContext context;

        public GetInvoiceByCustomerIdHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<IEnumerable<Invoice>> Handle(GetInvoiceByCustomerIdRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return GetInvoices();

            async Task<IEnumerable<Invoice>> GetInvoices()
            {
                var invoices = await context.Invoices.Where(x => x.CustomerId== request.CustomerId).ToListAsync(); 

                if (invoices is null)
                    throw new CustomException(HttpStatusCode.NotFound, $"Invoice with the customerId {request.CustomerId} cannot be found!");

                return invoices;
            }
        }
    }

}
