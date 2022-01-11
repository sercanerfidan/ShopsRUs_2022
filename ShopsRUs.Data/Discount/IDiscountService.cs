using ShopsRUs.Domain.Customers;
using ShopsRUs.Domain.Discounts;
using ShopsRUs.Domain.Enums;
using ShopsRUs.Domain.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopsRUs.Data.Discounts
{
    public interface IDiscountService
    {
        decimal CalculateDiscountForCustomerType(decimal amount, Customer customer, IEnumerable<Discount> discounts);

        decimal CalculateDiscountForAmount(decimal amount, IEnumerable<Discount> discounts);

        decimal CalculateDiscountForCategory(Product product, IEnumerable<Discount> discounts);

        decimal DecideDiscountAmount(Customer customer, Product product, IEnumerable<Discount> discounts);

        decimal CalculateDiscountForVeteranCustomers(decimal amount, Customer customer, IEnumerable<Discount> discounts);

        bool IsVeteran(DateTime customerCreateDate);

    }
}