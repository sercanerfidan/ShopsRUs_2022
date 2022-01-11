using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Domain.Discounts;
using ShopsRUs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Data.Mappings
{
    class DiscountMap : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("DISCOUNT", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnName("NAME").IsRequired();
            builder.Property(x => x.CustomerType).HasColumnName("CUSTOMER_TYPE").IsRequired();
            builder.Property(x => x.Percent).HasColumnName("PERCENT").IsRequired();
            builder.Property(x => x.Type).HasColumnName("TYPE").HasConversion(v => v.ToString(), v => (DiscountType)Enum.Parse(typeof(DiscountType), v));
            builder.Property(x => x.ProductType).HasColumnName("PRODUCT_TYPE").HasConversion(v => v.ToString(), v => (Category)Enum.Parse(typeof(Category), v));
            builder.Property(x => x.Amount).HasColumnName("AMOUNT").IsRequired();
            builder.Property(x => x.TriggerAmount).HasColumnName("TRIGGER_AMOUNT").IsRequired();
            builder.Property(x => x.CreateDate).HasColumnName("CREATE_DATE").IsRequired();

        }
    }
}
