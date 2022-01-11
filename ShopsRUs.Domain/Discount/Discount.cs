using ShopsRUs.Domain.Enums;
using ShopsRUs.Domain.Model;
using System;

namespace ShopsRUs.Domain.Discounts
{
    public class Discount : BaseModel
    {
        public Discount(string name, CustomerType customerType, Category productType, int percent, DiscountType type, decimal amount, decimal triggerAmount)
        {
            Name = name;
            CustomerType = customerType;
            ProductType = productType;
            Percent = percent;
            Type = type;
            Amount = amount;
            TriggerAmount = amount;
        }
        public string Name { get; set; }

        public CustomerType CustomerType { get; set; }

        public Category ProductType { get; set; }

        public int Percent { get; set; }

        public DiscountType Type { get; set; }

        public decimal Amount { get; set; }

        public decimal TriggerAmount { get; set; }

    }
}
