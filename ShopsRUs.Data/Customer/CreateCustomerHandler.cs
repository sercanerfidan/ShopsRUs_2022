using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Domain.Customers;
using ShopsRUs.Domain.Enums;
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
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerRequest, Customer>
    {

        private readonly ApplicationDbContext context;

        public CreateCustomerHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<Customer> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return CreateCustomer();

            async Task<Customer> CreateCustomer()
            {
                var customer = new Customer(request.Name, request.Surname, Enum.Parse<CustomerType>(request.Type));

                await context.Customers.AddAsync(customer, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return customer;
            }
        }
    }

}
