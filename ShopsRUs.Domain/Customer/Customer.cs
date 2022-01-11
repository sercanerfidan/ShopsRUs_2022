using ShopsRUs.Domain.Enums;
using ShopsRUs.Domain.Model;
using System;

namespace ShopsRUs.Domain.Customers
{
    public class Customer : BaseModel
    {

        public Customer(string name, string surname, CustomerType type)
        {
            Name = name;
            Surname = surname;
            Type = type;
        }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public CustomerType Type { get; private set; }

    }
}
