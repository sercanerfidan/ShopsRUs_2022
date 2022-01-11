using FluentValidation;
using MediatR;
using ShopsRUs.Domain.Enums;
using ShopsRUs.Domain.Orders;
using ShopsRUs.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Domain.Orders
{
    public record CreateOrderRequest(int CustomerId, int ProductId) : IRequest<Order>;

    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(r => r.CustomerId).NotEmpty().WithMessage("CustomerId name cannot be empty!");
            RuleFor(r => r.CustomerId).GreaterThan(0).WithMessage("CustomerId must be grather then 0!");
            RuleFor(r => r.ProductId).NotEmpty().WithMessage("ProductId cannot be empty!");
            RuleFor(r => r.ProductId).GreaterThan(0).WithMessage("ProductId must be grather then 0!");
        }
    }
}
