using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Domain.Customers;
using ShopsRUs.Domain.Enums;
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
    public class AddProductHandler : IRequestHandler<AddProductRequest, Product>
    {

        private readonly ApplicationDbContext context;

        public AddProductHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<Product> Handle(AddProductRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return AddProduct();

            async Task<Product> AddProduct()
            {
                var product = new Product(request.Name, request.Price, Enum.Parse<Category>(request.Category));

                await context.Products.AddAsync(product, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return product;
            }
        }
    }

}
