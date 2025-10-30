using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStockBackend.Core.Entities;

namespace SmartStockBackend.Infra.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(p => p.SKU)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Quantity)
                   .IsRequired();

            builder.Property(p => p.MinimumQuantity)
                   .IsRequired();

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(10,2)");

            builder.Property(p => p.LastRestockDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasMany(p => p.Movements)
                   .WithOne(m => m.Product)
                   .HasForeignKey(m => m.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
