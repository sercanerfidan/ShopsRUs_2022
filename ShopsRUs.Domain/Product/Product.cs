using ShopsRUs.Domain.Enums;
using ShopsRUs.Domain.Invoices;
using ShopsRUs.Domain.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace ShopsRUs.Domain.Products
{
    public class Product : BaseModel
    {

        public Product(string name, decimal price, Category category)
        {
            Name = name;
            Price = price;
            Category = category;

        }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public Category Category{ get; private set; }

    }
}
