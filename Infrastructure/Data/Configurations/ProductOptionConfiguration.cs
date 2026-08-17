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
    public class ProductOptionConfiguration : IEntityTypeConfiguration<ProductOption>
    {
        public void Configure(EntityTypeBuilder<ProductOption> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(x => x.Name)
                .IsUnique();

          
        }
    }
}
