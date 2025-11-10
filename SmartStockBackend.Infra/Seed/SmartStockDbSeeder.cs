using SmartStockBackend.Core.Entities;
using SmartStockBackend.Core.Enums;

namespace SmartStockBackend.Infra.Seed
{
    public static class SmartStockDbSeeder
    {
        public static void Seed(SmartStockDbContext context)
        {
            if (context.Products.Any() || context.StockMovements.Any())
                return;

            var rand = new Random();

            var products = new List<Product>
            {
                new() { Name = "Mouse Gamer", SKU = "PRD-001", Quantity = 150, MinimumQuantity = 20, Price = 120 },
                new() { Name = "Teclado Mecânico", SKU = "PRD-002", Quantity = 100, MinimumQuantity = 15, Price = 280 },
                new() { Name = "Monitor 24''", SKU = "PRD-003", Quantity = 60, MinimumQuantity = 10, Price = 950 },
                new() { Name = "Headset Pro", SKU = "PRD-004", Quantity = 120, MinimumQuantity = 18, Price = 310 },
                new() { Name = "Webcam HD", SKU = "PRD-005", Quantity = 90, MinimumQuantity = 12, Price = 200 }
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            var startDate = DateTime.UtcNow.AddDays(-60);
            var movements = new List<StockMovement>();

            foreach (var product in products)
            {
                int currentQuantity = product.Quantity;

                for (int i = 0; i < 60; i++)
                {
                    var date = startDate.AddDays(i);
                    var dayOfWeek = (int)date.DayOfWeek;

                    double demandMultiplier = (dayOfWeek == 5 || dayOfWeek == 6) ? 1.3 : 1.0;
                    double seasonFactor = 1 + 0.15 * Math.Sin(i / 30.0);
                    var isExit = rand.NextDouble() < 0.8;
                    var type = isExit ? MovementTypeStockMovementEnum.Exit : MovementTypeStockMovementEnum.Entry;

                    int amount = isExit
                        ? rand.Next(1, 12)
                        : rand.Next(8, 25);

                    amount = (int)Math.Round(amount * demandMultiplier * seasonFactor);

                    if (currentQuantity <= product.MinimumQuantity)
                    {
                        type = MovementTypeStockMovementEnum.Entry;
                        amount = rand.Next(20, 60);
                    }

                    if (type == MovementTypeStockMovementEnum.Exit)
                        currentQuantity = Math.Max(0, currentQuantity - amount);
                    else
                        currentQuantity += amount;
                    movements.Add(new StockMovement
                    {
                        ProductId = product.Id,
                        Type = type,
                        Quantity = amount,
                        MovementDate = date
                    });
                }
                product.Quantity = currentQuantity;
            }

            context.StockMovements.AddRange(movements);
            context.SaveChanges();
        }
    }
}
