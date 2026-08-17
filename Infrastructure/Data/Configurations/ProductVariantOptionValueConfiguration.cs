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
    public class ProductVariantOptionValueConfiguration
        : IEntityTypeConfiguration<ProductVariantOptionValue>
    {
        public void Configure(EntityTypeBuilder<ProductVariantOptionValue> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ProductVariant)
                .WithMany(x => x.OptionValues)
                .HasForeignKey(x => x.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ProductOptionValue)
                .WithMany(x => x.Variants)
                .HasForeignKey(x => x.ProductOptionValueId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.ProductVariantId,
                x.ProductOptionValueId
            })
            .IsUnique();
        }
    }
}
