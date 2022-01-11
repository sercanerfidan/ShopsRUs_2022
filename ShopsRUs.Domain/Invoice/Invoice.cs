using ShopsRUs.Domain.Model;
using ShopsRUs.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopsRUs.Domain.Invoices
{
    public class Invoice : BaseModel
    {

        public Invoice(decimal amount, decimal discount, decimal netAmount, int customerId, int productId, int orderId)
        {
            Amount = amount;
            Discount = discount;
            NetAmount = netAmount;
            CustomerId = customerId;
            ProductId = productId;
            OrderId = orderId;
        }

        public decimal Amount { get; private set; }

        public decimal Discount { get; private set; }

        public decimal NetAmount { get; private set; }

        public int CustomerId { get; private set; }

        public int ProductId { get; private set; }

        public int OrderId { get; private set; }


    }
}
