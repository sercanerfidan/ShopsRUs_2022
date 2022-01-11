using MediatR;
using ShopsRUs.Domain.Discounts;
using ShopsRUs.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Domain.Discounts
{
    public record GetDiscountsRequest() : IRequest<IEnumerable<Discount>>;
}
