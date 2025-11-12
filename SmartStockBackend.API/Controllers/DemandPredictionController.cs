using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartStockBackend.Application.Services.DemandPredictionServices;
using SmartStockBackend.Application.Services.DemandPredictionServices.ViewModels;
using SmartStockBackend.Core.Models;

namespace SmartStockBackend.API.Controllers
{
    [ApiController, Route("api/demand-prediction"), Authorize]
    public class DemandPredictionController : ControllerBase
    {
        private readonly IDemandPredictionService _service;

        public DemandPredictionController(IDemandPredictionService service)
        {
            _service = service;
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetDemandPrediction(int id, CancellationToken cancellationToken)
        {
            var data = await _service.DemandPredictionProduct(id, cancellationToken);
            return Ok(new ApiResponse<DemandPredictionProductViewDto>
            {
                Status = true,
                Message = null,
                Data = data
            });
        }
    }
}
