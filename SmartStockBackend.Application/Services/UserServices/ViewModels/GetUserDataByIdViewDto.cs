namespace SmartStockBackend.Application.Services.UserServices.ViewModels
{
    public class GetUserDataByIdViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
