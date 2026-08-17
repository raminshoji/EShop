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
    public class ProductOptionValueConfiguration : IEntityTypeConfiguration<ProductOptionValue>
    {
        public void Configure(EntityTypeBuilder<ProductOptionValue> builder)
        {
            builder.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(x => new
            {
                x.ProductOptionId,
                x.Value
            })
            .IsUnique();

            builder.HasOne(x => x.ProductOption)
                .WithMany(x => x.Values)
                .HasForeignKey(x => x.ProductOptionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
