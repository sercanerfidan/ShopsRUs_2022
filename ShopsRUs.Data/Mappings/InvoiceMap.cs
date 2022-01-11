using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Domain.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Data.Mappings
{
    class InvoiceMap : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("INVOICE", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount).HasColumnName("AMOUNT").IsRequired();
            builder.Property(x => x.Discount).HasColumnName("DISCOUNT").IsRequired();
            builder.Property(x => x.NetAmount).HasColumnName("NET_AMOUNT").IsRequired();
            builder.Property(x => x.CustomerId).HasColumnName("CUSTOMER_ID").IsRequired();
            builder.Property(x => x.ProductId).HasColumnName("PRODUCT_ID").IsRequired();
            builder.Property(x => x.OrderId).HasColumnName("ORDER_ID").IsRequired();
            builder.Property(x => x.CreateDate).HasColumnName("CREATE_DATE").IsRequired();

        }
    }
}
