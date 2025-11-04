namespace SmartStockBackend.Application.Services.ProductServices.InputModels
{
    public class UpdateProductInputDto
    {
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int MinimumQuantity { get; set; }
        public decimal Price { get; set; }
        public DateTime LastRestockDate { get; set; }
    }
}
