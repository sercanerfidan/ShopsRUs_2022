using MediatR;
using ShopsRUs.Domain.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Domain.Invoices
{
    public record GetInvoiceByOrderIdRequest(int OrderId) : IRequest<Invoice>;
}
