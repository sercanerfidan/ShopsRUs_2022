using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Domain.Customers;
using ShopsRUs.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopsRUs.Data.Customers
{
    public class GetCustomerHandler : IRequestHandler<GetCustomerRequest, Customer>
    {

        private readonly ApplicationDbContext context;

        public GetCustomerHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<Customer> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return GetCustomer();

            async Task<Customer> GetCustomer()
            {
                var customer = await context.Customers.Where(x => x.Id == request.Id).FirstOrDefaultAsync(); 

                if (customer is null)
                    throw new CustomException(HttpStatusCode.NotFound, $"Customer with the id {request.Id} cannot be found!");

                return customer;
            }
        }
    }

}
