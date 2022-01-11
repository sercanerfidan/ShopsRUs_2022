using FluentValidation;
using MediatR;
using ShopsRUs.Domain.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Domain.Invoices
{
    public record CreateInvoiceRequest(decimal Amount, decimal Discount, decimal NetAmount, int CustomerId, int ProductId, int OrderId) : IRequest<Invoice>;

    public class CreateInvoiceRequestValidator : AbstractValidator<CreateInvoiceRequest>
    {
        public CreateInvoiceRequestValidator()
        {
        }
    }
}
