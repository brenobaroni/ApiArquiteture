

using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.EntityConfigurations
{
    public class ProductTypeConfigurations
    {
        public class ProductConfiguration : IEntityTypeConfiguration<Product>
        {
            public void Configure(EntityTypeBuilder<Product> builder)
            {
                builder.ToTable("products", "shop");
                builder.HasMany(s => s.sale_items).WithOne(s => s.product).HasForeignKey(s => s.sale_id);
            }
        }
    }
}
