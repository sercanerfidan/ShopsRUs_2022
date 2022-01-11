using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Domain.Customers;
using ShopsRUs.Domain.Discounts;
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

namespace ShopsRUs.Data.Discounts
{
    public class AddDiscountHandler : IRequestHandler<AddDiscountRequest, Discount>
    {

        private readonly ApplicationDbContext context;

        public AddDiscountHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<Discount> Handle(AddDiscountRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return AddDiscount();

            async Task<Discount> AddDiscount()
            {
                var discount = new Discount(request.Name, Enum.Parse<CustomerType>(request.CustomerType), Enum.Parse<Category>(request.ProductType), request.Percent, Enum.Parse<DiscountType>(request.DiscountType), request.Amount, request.TriggerAmount);

                await context.Discounts.AddAsync(discount, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return discount;
            }
        }
    }

}
