using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartStockBackend.Application.Services.ProductServices;
using SmartStockBackend.Application.Services.ProductServices.InputModels;
using SmartStockBackend.Application.Services.ProductServices.ViewModels;
using SmartStockBackend.Core.Entities;
using SmartStockBackend.Core.Models;

namespace SmartStockBackend.API.Controllers
{
    [ApiController, Route("api/products"), Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductInputDto model, CancellationToken cancellationToken)
        {
            await _service.Create(model, cancellationToken);
            return Ok(new ApiResponse<object>
            {
                Status = true,
                Message = "Product successfully registered!",
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
                Message = "Product successfully deleted!",
                Data = null
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var data = await _service.GetAll(cancellationToken);
            return Ok(new ApiResponse<List<ProductDataViewDto>>
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
            return Ok(new ApiResponse<Product>
            {
                Status = true,
                Message = null,
                Data = data
            });
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductInputDto model, CancellationToken cancellationToken)
        {
            await _service.Update(id, model, cancellationToken);
            return Ok(new ApiResponse<object>
            {
                Status = true,
                Message = "Product successfully updated!",
                Data = null
            });
        }
    }
}
