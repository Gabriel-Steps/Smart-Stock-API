using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartStockBackend.Application.Services.UserServices;
using SmartStockBackend.Application.Services.UserServices.InputModels;
using SmartStockBackend.Application.Services.UserServices.ViewModels;
using SmartStockBackend.Core.Models;

namespace SmartStockBackend.API.Controllers
{
    [ApiController, Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }

        [HttpPost, Route("register")]
        public async Task<ActionResult> Register(RegisterUserInputDto model, CancellationToken cancellationToken)
        {
            var data = await _service.Register(model, cancellationToken);
            return Ok(new ApiResponse<AccessUserViewDto>
            {
                Status = true,
                Message = "Registration completed successfully!",
                Data = data
            });
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(LoginUserInputDto model, CancellationToken cancellationToken)
        {
            var data = await _service.Login(model, cancellationToken);
            return Ok(new ApiResponse<AccessUserViewDto>
            {
                Status = true,
                Message = "Login successful!",
                Data = data
            });
        }
    }
}
