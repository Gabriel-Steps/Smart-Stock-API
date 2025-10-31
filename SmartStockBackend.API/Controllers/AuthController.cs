using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartStockBackend.Application.Services.UserServices;
using SmartStockBackend.Application.Services.UserServices.InputModels;
using SmartStockBackend.Application.Services.UserServices.ViewModels;
using SmartStockBackend.Core.Exceptions.UserExceptions;
using SmartStockBackend.Core.Models;

namespace SmartStockBackend.API.Controllers
{
    [ApiController, Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService service, ILogger<AuthController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost, Route("register")]
        public async Task<ActionResult> Register(RegisterUserInputDto model)
        {
            try
            {
                var data = await _service.Register(model);
                return Ok(new ApiResponse<AccessUserViewDto>
                {
                    Status = true,
                    Message = "Registration completed successfully!",
                    Data = data
                });
            } catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ApiResponse<object>
                {
                    Status = false,
                    Message = "Failure while registering user!",
                    Data = null
                });
            }
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(LoginUserInputDto model)
        {
            try
            {
                var data = await _service.Login(model);
                return Ok(new ApiResponse<AccessUserViewDto>
                {
                    Status = true,
                    Message = "Login successful!",
                    Data = data
                });
            }
            catch(UserNotFoundException ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(new ApiResponse<object>
                {
                    Status = false,
                    Message = $"User with e-mail {model.Email} not found",
                    Data = null
                });
            }catch(UserValidationException ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(new ApiResponse<object>
                {
                    Status = false,
                    Message = $"Invalid email or password",
                    Data = null
                });
            }
        }
    }
}
