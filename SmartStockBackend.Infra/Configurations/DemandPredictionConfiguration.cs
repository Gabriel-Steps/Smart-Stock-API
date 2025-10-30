using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStockBackend.Core.Entities;

namespace SmartStockBackend.Infra.Configurations
{
    public class DemandPredictionConfiguration : IEntityTypeConfiguration<DemandPrediction>
    {
        public void Configure(EntityTypeBuilder<DemandPrediction> builder)
        {
            builder.ToTable("DemandPredictions");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.PredictionDate)
                   .IsRequired();

            builder.Property(d => d.PredictedDaysToDepletion)
                   .IsRequired();

            builder.HasOne(d => d.Product)
                   .WithMany()
                   .HasForeignKey(d => d.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
