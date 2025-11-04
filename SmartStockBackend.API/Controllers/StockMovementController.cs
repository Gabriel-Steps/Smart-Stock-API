using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartStockBackend.Application.Services.StockMovementServices;
using SmartStockBackend.Application.Services.StockMovementServices.InputModels;
using SmartStockBackend.Application.Services.StockMovementServices.ViewModels;
using SmartStockBackend.Core.Models;

namespace SmartStockBackend.API.Controllers
{
    [ApiController, Route("api/stock-movements"), Authorize]
    public class StockMovementController : ControllerBase
    {
        private readonly IStockMovementService _service;

        public StockMovementController(IStockMovementService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStockMovementInputDto model, CancellationToken cancellationToken)
        {
            await _service.Create(model, cancellationToken);
            return Ok(new ApiResponse<object>
            {
                Status = true,
                Message = "Move completed successfully!",
                Data = null
            });
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _service.Delete(id, cancellationToken);
            return Ok(new ApiResponse<object>
            {
                Status = true,
                Message = "Movement removed from history!",
                Data = null
            });
        }

        [HttpGet, Route("history/{id}")]
        public async Task<IActionResult> GetAll(int id, CancellationToken cancellationToken)
        {
            var data = await _service.GetAll(id, cancellationToken);
            return Ok(new ApiResponse<List<StockMovementDataViewDto>>
            {
                Status = true,
                Message = null,
                Data = data
            });
        }
    }
}
