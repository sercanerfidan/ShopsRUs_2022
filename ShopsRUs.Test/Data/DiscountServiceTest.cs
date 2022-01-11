using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Data;
using ShopsRUs.Domain.Customers;
using ShopsRUs.Domain.Enums;
using ShopsRUs.Domain.Discounts;
using MediatR;
using ShopsRUs.Data.Discounts;
using ShopsRUs.Domain.Products;

namespace ShopsRUs.Test.Data
{
    public class DiscountServiceTest
    {

        [Fact]
        public async Task ReturnCorrectDiscountAmountForCustomerTypeAffilate() {

            var dbName = Guid.NewGuid().ToString();
            var customer = new Customer(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), CustomerType.Affilate) { Id = new Random().Next(), CreateDate = DateTime.Now.Date};
            decimal productPrice = 1000;
            var discount1 = new Discount(Guid.NewGuid().ToString(), CustomerType.Affilate, Category.None, 30, DiscountType.Customer, 0, 0);
            var discount2 = new Discount(Guid.NewGuid().ToString(), CustomerType.Employee, Category.None, 10, DiscountType.Customer, 0, 0);
            var discount3 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 0, DiscountType.Customer, 0, 0);

            var mediator = new Mock<IMediator>();

            using (var context = ContextHelper.Create(dbName))
            {

                await context.Discounts.AddAsync(discount1);
                await context.Discounts.AddAsync(discount2);
                await context.Discounts.AddAsync(discount3);
                await context.SaveChangesAsync();
                var discountService = new DiscountSerivce(mediator.Object);
                var discountAmount = discountService.CalculateDiscountForCustomerType(productPrice, customer, (IEnumerable<Discount>)context.Discounts);

                Assert.Equal(300, discountAmount);
            }
        }

        [Fact]
        public async Task ReturnCorrectDiscountAmountForCustomerTypeEmployee()
        {

            var dbName = Guid.NewGuid().ToString();
            var customer = new Customer(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), CustomerType.Employee) { Id = new Random().Next(), CreateDate = DateTime.Now.Date };
            decimal productPrice = 1000;
            var discount1 = new Discount(Guid.NewGuid().ToString(), CustomerType.Affilate, Category.None, 30, DiscountType.Customer, 0, 0);
            var discount2 = new Discount(Guid.NewGuid().ToString(), CustomerType.Employee, Category.None, 10, DiscountType.Customer, 0, 0);
            var discount3 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 0, DiscountType.Customer, 0, 0);


            var mediator = new Mock<IMediator>();

            using (var context = ContextHelper.Create(dbName))
            {

                await context.Discounts.AddAsync(discount1);
                await context.Discounts.AddAsync(discount2);
                await context.Discounts.AddAsync(discount3);

                await context.SaveChangesAsync();
                var discountService = new DiscountSerivce(mediator.Object);
                var discountAmount = discountService.CalculateDiscountForCustomerType(productPrice, customer, (IEnumerable<Discount>)context.Discounts);

                Assert.Equal(100 , discountAmount);
            }
        }

        [Fact]
        public async Task ReturnCorrectDiscountAmountWhenCustomerNotAffilateOrEmpolyee()
        {

            var dbName = Guid.NewGuid().ToString();
            var customer = new Customer(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), CustomerType.Standart) { Id = new Random().Next(), CreateDate = DateTime.Now.Date };
            decimal productPrice = 1000;
            var discount1 = new Discount(Guid.NewGuid().ToString(), CustomerType.Affilate, Category.None, 30, DiscountType.Customer, 0, 0);
            var discount2 = new Discount(Guid.NewGuid().ToString(), CustomerType.Employee, Category.None, 10, DiscountType.Customer, 0, 0);
            var discount3 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 0, DiscountType.Customer, 0, 0);


            var mediator = new Mock<IMediator>();

            using (var context = ContextHelper.Create(dbName))
            {

                await context.Discounts.AddAsync(discount1);
                await context.Discounts.AddAsync(discount2);
                await context.Discounts.AddAsync(discount3);

                await context.SaveChangesAsync();
                var discountService = new DiscountSerivce(mediator.Object);
                var discountAmount = discountService.CalculateDiscountForCustomerType(productPrice, customer, (IEnumerable<Discount>)context.Discounts);

                Assert.Equal(0, discountAmount);
            }
        }

        [Fact]
        public async Task ReturnCorrectDiscountAmountForVeteranCustomerWhenGreatherThanTwoYears()
        {

            var dbName = Guid.NewGuid().ToString();
            var customer = new Customer(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), CustomerType.Standart) { Id = new Random().Next(), CreateDate = DateTime.Now.Date.AddYears(-3) };
            decimal productPrice = 1000;
            var discount1 = new Discount(Guid.NewGuid().ToString(), CustomerType.Affilate, Category.None, 30, DiscountType.Customer, 0, 0);
            var discount2 = new Discount(Guid.NewGuid().ToString(), CustomerType.Employee, Category.None, 10, DiscountType.Customer, 0, 0);
            var discount3 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 0, DiscountType.Customer, 0, 0);
            var discount4 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 5, DiscountType.Veteran, 0, 0);

            var mediator = new Mock<IMediator>();

            using (var context = ContextHelper.Create(dbName))
            {

                await context.Discounts.AddAsync(discount1);
                await context.Discounts.AddAsync(discount2);
                await context.Discounts.AddAsync(discount3);
                await context.Discounts.AddAsync(discount4);

                await context.SaveChangesAsync();
                var discountService = new DiscountSerivce(mediator.Object);
                var discountAmount = discountService.CalculateDiscountForVeteranCustomers(productPrice, customer, (IEnumerable<Discount>)context.Discounts);

                Assert.Equal(50, discountAmount);
            }
        }

        [Fact]
        public async Task ReturnCorrectMaxDiscountBothAffilateAndVeteran()
        {

            var dbName = Guid.NewGuid().ToString();
            var customer = new Customer(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), CustomerType.Affilate) { Id = new Random().Next(), CreateDate = DateTime.Now.Date.AddYears(-3) };
            var product = new Product(Guid.NewGuid().ToString(), 1000, Category.Jewelry) { Id = new Random().Next(), CreateDate = DateTime.Now.Date };
          
            var discount1 = new Discount(Guid.NewGuid().ToString(), CustomerType.Affilate, Category.None, 30, DiscountType.Customer, 0, 0);
            var discount2 = new Discount(Guid.NewGuid().ToString(), CustomerType.Employee, Category.None, 10, DiscountType.Customer, 0, 0);
            var discount3 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 0, DiscountType.Customer, 0, 0);
            var discount4 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 5, DiscountType.Veteran, 0, 0);

            var mediator = new Mock<IMediator>();

            using (var context = ContextHelper.Create(dbName))
            {

                await context.Discounts.AddAsync(discount1);
                await context.Discounts.AddAsync(discount2);
                await context.Discounts.AddAsync(discount3);
                await context.Discounts.AddAsync(discount4);

                await context.SaveChangesAsync();
                var discountService = new DiscountSerivce(mediator.Object);
                var discountAmount = discountService.DecideDiscountAmount(customer, product, (IEnumerable<Discount>)context.Discounts);

                Assert.Equal(300, discountAmount);
            }
        }


        [Fact]
        public async Task ReturnCorrectDiscountBothAffilateAndVeteranAndDiscount5Per100()
        {

            var dbName = Guid.NewGuid().ToString();
            var customer = new Customer(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), CustomerType.Affilate) { Id = new Random().Next(), CreateDate = DateTime.Now.Date.AddYears(-3) };
            var product = new Product(Guid.NewGuid().ToString(), 1000, Category.Jewelry) { Id = new Random().Next(), CreateDate = DateTime.Now.Date };

            var discount1 = new Discount(Guid.NewGuid().ToString(), CustomerType.Affilate, Category.None, 30, DiscountType.Customer, 0, 0);
            var discount2 = new Discount(Guid.NewGuid().ToString(), CustomerType.Employee, Category.None, 10, DiscountType.Customer, 0, 0);
            var discount3 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 0, DiscountType.Customer, 0, 0);
            var discount4 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 5, DiscountType.Veteran, 0, 0);
            var discount5 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 0, DiscountType.Amount, 5, 100);


            var mediator = new Mock<IMediator>();

            using (var context = ContextHelper.Create(dbName))
            {

                await context.Discounts.AddAsync(discount1);
                await context.Discounts.AddAsync(discount2);
                await context.Discounts.AddAsync(discount3);
                await context.Discounts.AddAsync(discount4);
                await context.Discounts.AddAsync(discount5);

                await context.SaveChangesAsync();
                var discountService = new DiscountSerivce(mediator.Object);
                var discountAmount = discountService.DecideDiscountAmount(customer, product, (IEnumerable<Discount>)context.Discounts);

                Assert.Equal(350, discountAmount);
            }
        }



        [Fact]
        public async Task ReturnCorrectDiscountOnlyAmountBasedDiscount()
        {

            var dbName = Guid.NewGuid().ToString();
            var customer = new Customer(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), CustomerType.Standart) { Id = new Random().Next(), CreateDate = DateTime.Now.Date.AddYears(-1) };
            var product = new Product(Guid.NewGuid().ToString(), 1000, Category.Jewelry) { Id = new Random().Next(), CreateDate = DateTime.Now.Date };

            var discount1 = new Discount(Guid.NewGuid().ToString(), CustomerType.Affilate, Category.None, 30, DiscountType.Customer, 0, 0);
            var discount2 = new Discount(Guid.NewGuid().ToString(), CustomerType.Employee, Category.None, 10, DiscountType.Customer, 0, 0);
            var discount3 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 0, DiscountType.Customer, 0, 0);
            var discount4 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 5, DiscountType.Veteran, 0, 0);
            var discount5 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 0, DiscountType.Amount, 5, 100);


            var mediator = new Mock<IMediator>();

            using (var context = ContextHelper.Create(dbName))
            {

                await context.Discounts.AddAsync(discount1);
                await context.Discounts.AddAsync(discount2);
                await context.Discounts.AddAsync(discount3);
                await context.Discounts.AddAsync(discount4);
                await context.Discounts.AddAsync(discount5);

                await context.SaveChangesAsync();
                var discountService = new DiscountSerivce(mediator.Object);
                var discountAmount = discountService.DecideDiscountAmount(customer, product, (IEnumerable<Discount>)context.Discounts);

                Assert.Equal(50, discountAmount);
            }
        }


        [Fact]
        public async Task ReturnCorrectDiscountVeteranAndAmountBasedDiscount5Per100()
        {

            var dbName = Guid.NewGuid().ToString();
            var customer = new Customer(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), CustomerType.Standart) { Id = new Random().Next(), CreateDate = DateTime.Now.Date.AddYears(-3) };
            var product = new Product(Guid.NewGuid().ToString(), 1000, Category.Jewelry) { Id = new Random().Next(), CreateDate = DateTime.Now.Date };

            var discount1 = new Discount(Guid.NewGuid().ToString(), CustomerType.Affilate, Category.None, 30, DiscountType.Customer, 0, 0);
            var discount2 = new Discount(Guid.NewGuid().ToString(), CustomerType.Employee, Category.None, 10, DiscountType.Customer, 0, 0);
            var discount3 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 0, DiscountType.Customer, 0, 0);
            var discount4 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 5, DiscountType.Veteran, 0, 0);
            var discount5 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 0, DiscountType.Amount, 5, 100);


            var mediator = new Mock<IMediator>();

            using (var context = ContextHelper.Create(dbName))
            {

                await context.Discounts.AddAsync(discount1);
                await context.Discounts.AddAsync(discount2);
                await context.Discounts.AddAsync(discount3);
                await context.Discounts.AddAsync(discount4);
                await context.Discounts.AddAsync(discount5);

                await context.SaveChangesAsync();
                var discountService = new DiscountSerivce(mediator.Object);
                var discountAmount = discountService.DecideDiscountAmount(customer, product, (IEnumerable<Discount>)context.Discounts);

                Assert.Equal(100, discountAmount);
            }
        }


        [Fact]
        public async Task ReturnOnlyAmountBasedDiscountCustomerTypeAffilateWhenCategoryIsGroccery()
        {

            var dbName = Guid.NewGuid().ToString();
            var customer = new Customer(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), CustomerType.Affilate) { Id = new Random().Next(), CreateDate = DateTime.Now.Date.AddYears(-3) };
            var product = new Product(Guid.NewGuid().ToString(), 1000, Category.Grocery) { Id = new Random().Next(), CreateDate = DateTime.Now.Date };

            var discount1 = new Discount(Guid.NewGuid().ToString(), CustomerType.Affilate, Category.None, 30, DiscountType.Customer, 0, 0);
            var discount2 = new Discount(Guid.NewGuid().ToString(), CustomerType.Employee, Category.None, 10, DiscountType.Customer, 0, 0);
            var discount3 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 0, DiscountType.Customer, 0, 0);
            var discount4 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 5, DiscountType.Veteran, 0, 0);
            var discount5 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 0, DiscountType.Amount, 5, 100);
            var discount6 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.Grocery, 0, DiscountType.Product, 0, 0);


            var mediator = new Mock<IMediator>();

            using (var context = ContextHelper.Create(dbName))
            {

                await context.Discounts.AddAsync(discount1);
                await context.Discounts.AddAsync(discount2);
                await context.Discounts.AddAsync(discount3);
                await context.Discounts.AddAsync(discount4);
                await context.Discounts.AddAsync(discount5);
                await context.Discounts.AddAsync(discount6);

                await context.SaveChangesAsync();
                var discountService = new DiscountSerivce(mediator.Object);
                var discountAmount = discountService.DecideDiscountAmount(customer, product, (IEnumerable<Discount>)context.Discounts);

                Assert.Equal(50, discountAmount);
            }
        }

        [Fact]
        public async Task ReturnZeroDiscountWhenCategoryIsGrocceryAndCustomerEmployee()
        {

            var dbName = Guid.NewGuid().ToString();
            var customer = new Customer(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), CustomerType.Employee) { Id = new Random().Next(), CreateDate = DateTime.Now.Date.AddYears(-3) };
            var product = new Product(Guid.NewGuid().ToString(), 1000, Category.Grocery) { Id = new Random().Next(), CreateDate = DateTime.Now.Date };

            var discount1 = new Discount(Guid.NewGuid().ToString(), CustomerType.Affilate, Category.None, 30, DiscountType.Customer, 0, 0);
            var discount2 = new Discount(Guid.NewGuid().ToString(), CustomerType.Employee, Category.None, 10, DiscountType.Customer, 0, 0);
            var discount3 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 0, DiscountType.Customer, 0, 0);
            var discount4 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 5, DiscountType.Veteran, 0, 0);
            var discount6 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.Grocery, 0, DiscountType.Product, 0, 0);


            var mediator = new Mock<IMediator>();

            using (var context = ContextHelper.Create(dbName))
            {

                await context.Discounts.AddAsync(discount1);
                await context.Discounts.AddAsync(discount2);
                await context.Discounts.AddAsync(discount3);
                await context.Discounts.AddAsync(discount4);
                await context.Discounts.AddAsync(discount6);

                await context.SaveChangesAsync();
                var discountService = new DiscountSerivce(mediator.Object);
                var discountAmount = discountService.DecideDiscountAmount(customer, product, (IEnumerable<Discount>)context.Discounts);

                Assert.Equal(0, discountAmount);
            }
        }

        [Fact]
        public async Task ReturnCorrrectDiscountForOnlyAmountBasedDiscount()
        {

            var dbName = Guid.NewGuid().ToString();
            var customer = new Customer(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), CustomerType.Standart) { Id = new Random().Next(), CreateDate = DateTime.Now.Date.AddYears(0) };
            var product = new Product(Guid.NewGuid().ToString(), 1000, Category.Jewelry) { Id = new Random().Next(), CreateDate = DateTime.Now.Date };

            var discount1 = new Discount(Guid.NewGuid().ToString(), CustomerType.Affilate, Category.None, 30, DiscountType.Customer, 0, 0);
            var discount2 = new Discount(Guid.NewGuid().ToString(), CustomerType.Employee, Category.None, 10, DiscountType.Customer, 0, 0);
            var discount3 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 0, DiscountType.Customer, 0, 0);
            var discount4 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 5, DiscountType.Veteran, 0, 0);
            var discount6 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.Jewelry, 0, DiscountType.Amount, 10, 100);


            var mediator = new Mock<IMediator>();

            using (var context = ContextHelper.Create(dbName))
            {

                await context.Discounts.AddAsync(discount1);
                await context.Discounts.AddAsync(discount2);
                await context.Discounts.AddAsync(discount3);
                await context.Discounts.AddAsync(discount4);
                await context.Discounts.AddAsync(discount6);

                await context.SaveChangesAsync();
                var discountService = new DiscountSerivce(mediator.Object);
                var discountAmount = discountService.DecideDiscountAmount(customer, product, (IEnumerable<Discount>)context.Discounts);

                Assert.Equal(100, discountAmount);
            }
        }

        [Fact]
        public async Task ReturnCorrectDiscountWhenCategoryIsNotGrocceryAndCustomerAffilate()
        {

            var dbName = Guid.NewGuid().ToString();
            var customer = new Customer(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), CustomerType.Affilate) { Id = new Random().Next(), CreateDate = DateTime.Now.Date.AddYears(-1) };
            var product = new Product(Guid.NewGuid().ToString(), 1000, Category.Technology) { Id = new Random().Next(), CreateDate = DateTime.Now.Date };

            var discount1 = new Discount(Guid.NewGuid().ToString(), CustomerType.Affilate, Category.None, 30, DiscountType.Customer, 0, 0);
            var discount2 = new Discount(Guid.NewGuid().ToString(), CustomerType.Employee, Category.None, 10, DiscountType.Customer, 0, 0);
            var discount3 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 0, DiscountType.Customer, 0, 0);
            var discount4 = new Discount(Guid.NewGuid().ToString(), CustomerType.Standart, Category.None, 5, DiscountType.Veteran, 0, 0);


            var mediator = new Mock<IMediator>();

            using (var context = ContextHelper.Create(dbName))
            {

                await context.Discounts.AddAsync(discount1);
                await context.Discounts.AddAsync(discount2);
                await context.Discounts.AddAsync(discount3);
                await context.Discounts.AddAsync(discount4);

                await context.SaveChangesAsync();
                var discountService = new DiscountSerivce(mediator.Object);
                var discountAmount = discountService.DecideDiscountAmount(customer, product, (IEnumerable<Discount>)context.Discounts);

                Assert.Equal(300, discountAmount);
            }
        }


    }
}
