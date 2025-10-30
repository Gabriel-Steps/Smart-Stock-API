﻿namespace SmartStockBackend.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int MinimumQuantity { get; set; }
        public decimal Price { get; set; }
        public DateTime LastRestockDate { get; set; }

        public ICollection<StockMovement> Movements { get; set; } = new List<StockMovement>();
    }

}
