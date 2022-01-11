using MediatR;
using ShopsRUs.Domain.Orders;
using ShopsRUs.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Domain.Orders
{
    public record GetOrdersRequest(int CustomerId) : IRequest<IEnumerable<Order>>;
}
