using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStockBackend.Core.Entities;

namespace SmartStockBackend.Infra.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(u => u.Phone)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(u => u.ImageUrl)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                   .IsRequired();

            builder.Property(u => u.Role)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasDefaultValue("User");
        }
    }
}
