using ShopsRUs.Domain.Model;
using ShopsRUs.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopsRUs.Domain.Orders
{
    public class Order : BaseModel
    {
        public Order(int customerId, int productId)
        {
            CustomerId = customerId;
            ProductId = productId;
        }
        public int CustomerId { get; private set; }

        public int ProductId { get; private set; }

    }
}
