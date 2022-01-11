using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Domain.Customers;
using ShopsRUs.Domain.Enums;
using System;

namespace ShopsRUs.Data.Mappings
{
    class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("CUSTOMER", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnName("NAME").IsRequired();
            builder.Property(x => x.Surname).HasColumnName("SURNAME").IsRequired();
            builder.Property(x => x.Type).HasColumnName("TYPE").HasConversion( v => v.ToString(), v => (CustomerType)Enum.Parse(typeof(CustomerType), v));     
            builder.Property(x => x.CreateDate).HasColumnName("CREATE_DATE").IsRequired();
        }
    }
}
