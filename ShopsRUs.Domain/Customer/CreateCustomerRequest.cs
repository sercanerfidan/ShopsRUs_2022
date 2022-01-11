using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Domain.Customers
{
    public record CreateCustomerRequest(string Name, string  Surname, string Type) : IRequest<Customer>;

    public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Name cannot be empty!");
            RuleFor(r => r.Surname).NotEmpty().WithMessage("Surname cannot be empty!");
            RuleFor(r => r.Type).NotEmpty().WithMessage("Type cannot be empty!");
            RuleFor(r => r.Type).IsInEnum().WithMessage("Invalid CustomerType!");
        }
    }
}
