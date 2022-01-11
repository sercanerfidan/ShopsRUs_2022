using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Domain.Enums;
using ShopsRUs.Domain.Orders;
using ShopsRUs.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Data.Mappings
{
    class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("ORDER", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CustomerId).HasColumnName("CUSTOMER_ID").IsRequired();
            builder.Property(x => x.ProductId).HasColumnName("PRODUCT_ID").IsRequired();
            builder.Property(x => x.CreateDate).HasColumnName("CREATE_DATE").IsRequired();

        }
    }
}
