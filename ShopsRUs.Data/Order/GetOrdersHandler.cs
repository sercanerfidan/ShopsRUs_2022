using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Domain.Customers;
using ShopsRUs.Domain.Exceptions;
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
    public class GetOrdersHandler : IRequestHandler<GetOrdersRequest, IEnumerable<Order>>
    {

        private readonly ApplicationDbContext context;

        public GetOrdersHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<IEnumerable<Order>> Handle(GetOrdersRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return GetOrders();

            async Task<IEnumerable<Order>> GetOrders()
            {
                var orders = await context.Orders.Where(x => x.CustomerId == request.CustomerId).ToListAsync(); 

                if (!orders.Any())
                    throw new CustomException(HttpStatusCode.NotFound, $"Order with the CustomerId {request.CustomerId} cannot be found!");

                return orders;
            }
        }
    }

}
