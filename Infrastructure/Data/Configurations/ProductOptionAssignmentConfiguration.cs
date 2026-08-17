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
    public class ProductOptionAssignmentConfiguration
     : IEntityTypeConfiguration<ProductOptionAssignment>
    {
        public void Configure(EntityTypeBuilder<ProductOptionAssignment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.OptionAssignments)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ProductOption)
                .WithMany(x => x.ProductAssignments)
                .HasForeignKey(x => x.ProductOptionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new
            {
                x.ProductId,
                x.ProductOptionId
            })
            .IsUnique();
        }
    }
}
