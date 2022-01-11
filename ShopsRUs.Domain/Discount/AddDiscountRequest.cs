using FluentValidation;
using MediatR;
using ShopsRUs.Domain.Enums;
using ShopsRUs.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Domain.Discounts
{
    public record AddDiscountRequest(string Name, string CustomerType, string ProductType, int Percent, string DiscountType, decimal Amount, decimal TriggerAmount) : IRequest<Discount>;

    public class AddDiscountRequestValidator : AbstractValidator<AddDiscountRequest>
    {
        public AddDiscountRequestValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Discount name cannot be empty!");
            RuleFor(r => r.CustomerType).NotEmpty().WithMessage("CustomerType cannot be empty!");
            RuleFor(r => r.CustomerType).IsInEnum().WithMessage("Invalid Customer type!");
            RuleFor(r => r.Percent).NotEmpty().WithMessage("Percent cannot be empty!");
            RuleFor(r => r.Percent).GreaterThanOrEqualTo(0).WithMessage("Percent must greater then or equal 0!");
            RuleFor(r => r.ProductType).IsInEnum().WithMessage("Invalid Product type!");
            RuleFor(r => r.DiscountType).IsInEnum().WithMessage("Invalid Dsicount type!");
            RuleFor(r => r.Amount).GreaterThanOrEqualTo(0).WithMessage("Amount must greater then or equal 0!");
        }
    }
}
