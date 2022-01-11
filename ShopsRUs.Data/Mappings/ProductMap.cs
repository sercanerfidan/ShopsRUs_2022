using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Domain.Enums;
using ShopsRUs.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Data.Mappings
{
    class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("PRODUCT", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnName("NAME").IsRequired();
            builder.Property(x => x.Price).HasColumnName("PRICE").IsRequired();
            builder.Property(x => x.Category).HasColumnName("CATEGORY").HasConversion(v => v.ToString(), v => (Category)Enum.Parse(typeof(Category), v));
            builder.Property(x => x.CreateDate).HasColumnName("CREATE_DATE").IsRequired();

        }
    }
}
