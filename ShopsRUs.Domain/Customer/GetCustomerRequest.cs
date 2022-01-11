using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Domain.Customers
{
    public record GetCustomerRequest(int Id) : IRequest<Customer>;
}
