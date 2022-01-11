using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Domain.Customers;
using ShopsRUs.Domain.Exceptions;
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
    public class GetProductsHandler : IRequestHandler<GetProductsRequest, IEnumerable<Product>>
    {

        private readonly ApplicationDbContext context;

        public GetProductsHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<IEnumerable<Product>> Handle(GetProductsRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return GetProducts();

            async Task<IEnumerable<Product>> GetProducts()
            {
                var products = await context.Products.ToListAsync(); 

                if (!products.Any())
                    throw new CustomException(HttpStatusCode.NotFound, $"Prodcuts cannot be found!");

                return products;
            }
        }
    }

}
