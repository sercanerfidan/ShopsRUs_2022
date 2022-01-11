using FluentValidation;
using MediatR;
using ShopsRUs.Domain.Enums;
using ShopsRUs.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Domain.Products
{
    public record AddProductRequest(string Name, decimal Price, string Category) : IRequest<Product>;

    public class AddProductRequestValidator : AbstractValidator<AddProductRequest>
    {
        public AddProductRequestValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Product name cannot be empty!");
            RuleFor(r => r.Price).NotEmpty().WithMessage("Price cannot be empty!");
            RuleFor(r => r.Price).GreaterThan(0).WithMessage("Price cannot be 0!");
            RuleFor(r => r.Price).ScalePrecision(2, 4).WithMessage("Price must not be more than 4 digits in total, with allowance for 2 decimals!");
            RuleFor(r => r.Category).NotEmpty().WithMessage("Product type cannot be empty!");
            RuleFor(r => r.Category).IsInEnum().WithMessage("Invalid Product type!");
        }
    }
}
