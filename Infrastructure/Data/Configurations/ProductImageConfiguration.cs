using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.Property(x => x.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.DisplayOrder)
                .IsRequired();

            builder.Property(x => x.IsPrimary)
                .IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ProductVariant)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new
            {
                x.ProductId,
                x.DisplayOrder
            });
        }
    }
}
