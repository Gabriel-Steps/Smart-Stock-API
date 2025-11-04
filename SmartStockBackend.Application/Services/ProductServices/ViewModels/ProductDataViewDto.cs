namespace SmartStockBackend.Application.Services.ProductServices.ViewModels
{
    public class ProductDataViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
