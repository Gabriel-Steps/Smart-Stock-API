using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartStockBackend.Application.Services.UserServices;
using SmartStockBackend.Application.Services.UserServices.InputModels;
using SmartStockBackend.Application.Services.UserServices.ViewModels;
using SmartStockBackend.Core.Models;

namespace SmartStockBackend.API.Controllers
{
    [ApiController, Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var data = await _service.GetAll(cancellationToken);
            return Ok(new ApiResponse<List<UserDataView>>
            {
                Status = true,
                Message = null,
                Data = data
            });
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var data = await _service.GetById(id, cancellationToken);
            return Ok(new ApiResponse<GetUserDataByIdViewDto>
            {
                Status = true,
                Message = null,
                Data = data
            });
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserInputDto model, CancellationToken cancellationToken)
        {
            await _service.Update(id, model, cancellationToken);
            return Ok(new ApiResponse<object>
            {
                Status = true,
                Message = "User successfully updated!",
                Data = null
            });
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _service.Delete(id, cancellationToken);
            return Ok(new ApiResponse<object>
            {
                Status = true,
                Message = "User successfully deleted!",
                Data = null
            });
        }
    }
}
