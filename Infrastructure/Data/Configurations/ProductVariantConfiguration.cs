using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.Property(x => x.SKU)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.VariantKey)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Price)
                .HasPrecision(18, 2);

            builder.Property(x => x.StockQuantity)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.RowVersion)
            .IsRowVersion();

            builder.HasIndex(x => x.SKU)
                .IsUnique();

            builder.HasIndex(x => new
            {
                x.ProductId,
                x.VariantKey
            })
            .IsUnique();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Variants)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
