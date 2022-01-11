using MediatR;
using ShopsRUs.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Domain.Products
{
    public record GetProductsRequest() : IRequest<IEnumerable<Product>>;
}
