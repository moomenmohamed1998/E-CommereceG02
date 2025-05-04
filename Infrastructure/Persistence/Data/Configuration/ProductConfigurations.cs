using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.Brand)
                 .WithMany(p => p.Products)
                 .HasForeignKey(p => p.BrandId);

            builder.HasOne(p => p.Type)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.TypeId);

            builder.Property(p => p.Price)
                .HasColumnType("decimal (10,3)");
        }
    }
}
