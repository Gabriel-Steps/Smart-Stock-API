using Microsoft.EntityFrameworkCore;
using SmartStockBackend.Core.Entities;
using System.Reflection;

namespace SmartStockBackend.Infra
{
    public class SmartStockDbContext : DbContext
    {
        public SmartStockDbContext(DbContextOptions<SmartStockDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<DemandPrediction> DemandPredictions { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
