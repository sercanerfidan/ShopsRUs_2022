using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Domain.Customers;
using ShopsRUs.Domain.Discounts;
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
    public class GetDiscountsHandler : IRequestHandler<GetDiscountsRequest, IEnumerable<Discount>>
    {

        private readonly ApplicationDbContext context;

        public GetDiscountsHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<IEnumerable<Discount>> Handle(GetDiscountsRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return GetDiscounts();

            async Task<IEnumerable<Discount>> GetDiscounts()
            {
                var discounts = await context.Discounts.ToListAsync(); 

                if (!discounts.Any())
                    throw new CustomException(HttpStatusCode.NotFound, $"Discounts cannot be found!");

                return discounts;
            }
        }
    }

}
