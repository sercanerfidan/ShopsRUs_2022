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
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, Product>
    {

        private readonly ApplicationDbContext context;

        public GetProductByIdHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<Product> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return GetProductById();

            async Task<Product> GetProductById()
            {
                var product = await context.Products.Where(x => x.Id == request.ProductId).FirstOrDefaultAsync(); 

                if (product is null)
                    throw new CustomException(HttpStatusCode.NotFound, $"Product with the id {request.ProductId} cannot be found! To see available products please call GetProducts before.");

                return product;
            }
        }
    }

}
