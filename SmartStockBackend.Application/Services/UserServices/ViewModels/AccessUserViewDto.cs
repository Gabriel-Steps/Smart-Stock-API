namespace SmartStockBackend.Application.Services.UserServices.ViewModels
{
    public class AccessUserViewDto
    {
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string? Token { get; set; }
    }
}
