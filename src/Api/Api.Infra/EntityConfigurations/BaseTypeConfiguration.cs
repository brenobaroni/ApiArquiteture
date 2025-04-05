using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Api.Domain.Entities;

namespace Api.Data.EntityConfigurations
{
    public class BaseDataConfiguration<T> : IEntityTypeConfiguration<T>
    where T : BaseData
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.create_at)
                   .HasDefaultValueSql("getdate()");
        }
    }

    public class BaseConfiguration<T> : IEntityTypeConfiguration<T>
    where T : Base
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(u => u.id);
            builder.Property(x => x.id).ValueGeneratedOnAdd();
        }
    }
}
