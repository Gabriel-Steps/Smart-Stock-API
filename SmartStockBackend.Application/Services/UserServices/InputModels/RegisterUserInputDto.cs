namespace SmartStockBackend.Application.Services.UserServices.InputModels
{
    public class RegisterUserInputDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
