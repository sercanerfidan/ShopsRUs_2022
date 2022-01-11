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
    public record PrepareInvoiceInfosRequest(int CustomerId, int ProductId, int OrderId) : IRequest<Invoice>;

    public class PrepareInvoiceInfosRequestValidator : AbstractValidator<PrepareInvoiceInfosRequest>
    {
        public PrepareInvoiceInfosRequestValidator()
        {
        }
    }
}
