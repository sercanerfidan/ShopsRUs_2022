using MediatR;
using ShopsRUs.Domain.Customers;
using ShopsRUs.Domain.Discounts;
using ShopsRUs.Domain.Enums;
using ShopsRUs.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopsRUs.Data.Discounts
{
    public class DiscountSerivce : IDiscountService
    {
        private readonly IMediator mediator;
        public DiscountSerivce(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public decimal CalculateDiscountForAmount(decimal amount, IEnumerable<Discount> discounts)
        {
            decimal discountAmount = 0;

        
            var discount = discounts.FirstOrDefault(x => x.Type == DiscountType.Amount);
            if (discount != null)
            {
                if (amount > discount.TriggerAmount)
                {
                    int mod = (int)amount % 100;
                    int netAmount = (int)amount - mod;
                    int discountCount = netAmount / 100;
                    discountAmount = discountCount * discount.Amount;
                }
            }

            return discountAmount;
        }

        public decimal CalculateDiscountForCustomerType(decimal amount, Customer customer, IEnumerable<Discount> discounts)
        {
            decimal discountAmount = 0;

            if (discounts.Any())
            {
                var discount = discounts.FirstOrDefault(x => x.CustomerType == customer.Type);
                discountAmount = amount * discount.Percent / 100;
            }

            return discountAmount;

        }

        public decimal CalculateDiscountForCategory(Product product, IEnumerable<Discount> discounts)
        {
            decimal discountAmount = -1;
            var discount = discounts.FirstOrDefault(x => x.ProductType == product.Category && x.Type == DiscountType.Product);
            if (discount != null)
            {
                discountAmount = product.Price * discount.Percent / 100;

            }

            return discountAmount;
        }

        public decimal CalculateDiscountForVeteranCustomers(decimal amount, Customer customer, IEnumerable<Discount> discounts)
        {
            decimal discountAmount = 0;

            if (IsVeteran(customer.CreateDate))
            {
                var discount = discounts.FirstOrDefault(x => x.Type == DiscountType.Veteran);
                if (discount != null)
                {
                    discountAmount = amount * discount.Percent / 100;

                }
            }

            return discountAmount;
        }

        public bool IsVeteran(DateTime customerCreateDate)
        {
            return DateTime.Now.Date > customerCreateDate.AddYears(2);
        }

        public decimal DecideDiscountAmount(Customer customer, Product product, IEnumerable<Discount> discounts)
        {

            decimal discountAmount = 0;

            var vetreanDiscount = CalculateDiscountForVeteranCustomers(product.Price, customer, discounts);

            var customerTypeDiscount = CalculateDiscountForCustomerType(product.Price, customer, discounts);

            var categoryDiscount = CalculateDiscountForCategory(product, discounts);

            if (categoryDiscount == -1 || categoryDiscount > 0) {
                discountAmount = Math.Max(vetreanDiscount, Math.Max(customerTypeDiscount, categoryDiscount));
            }

            var amountResult = CalculateDiscountForAmount(product.Price, discounts);
            discountAmount += amountResult;

            return discountAmount;
        }

    }
}
