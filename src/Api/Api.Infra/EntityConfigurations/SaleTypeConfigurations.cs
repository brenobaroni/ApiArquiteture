

using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.EntityConfigurations
{
    public class SaleTypeConfigurations
    {
        public class SaleConfiguration : IEntityTypeConfiguration<Sale>
        {
            public void Configure(EntityTypeBuilder<Sale> builder)
            {
                builder.ToTable("sales", "shop");
                builder.HasMany(s => s.sale_items).WithOne(s => s.sale).HasForeignKey(s => s.sale_id);

            }
        }

        public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
        {
            public void Configure(EntityTypeBuilder<SaleItem> builder)
            {
                builder.ToTable("sales_items", "shop");
                builder.Property(s => s.product_id).IsRequired();
                builder.Property(s => s.sale_id).IsRequired();
                builder.Property(s => s.price).IsRequired();
                builder.Property(s => s.quantity).IsRequired();
                builder.HasOne(s => s.sale).WithMany(s => s.sale_items).HasForeignKey(s => s.id);
                builder.HasOne(s => s.product).WithMany(s => s.sale_items).HasForeignKey(s => s.product_id);

            }
        }
    }
}
