using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStockBackend.Core.Entities;

namespace SmartStockBackend.Infra.Configurations
{
    public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            builder.ToTable("StockMovements");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Type)
                   .IsRequired();

            builder.Property(m => m.Quantity)
                   .IsRequired();

            builder.Property(m => m.MovementDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(m => m.Product)
                   .WithMany(p => p.Movements)
                   .HasForeignKey(m => m.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
