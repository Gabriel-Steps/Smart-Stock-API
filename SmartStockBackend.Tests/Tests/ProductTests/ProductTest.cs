using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using SmartStockBackend.API.Controllers;
using SmartStockBackend.Application.Services.ProductServices;
using SmartStockBackend.Application.Services.ProductServices.InputModels;
using SmartStockBackend.Core.Models;

namespace SmartStockBackend.Tests.Tests.ProductTests
{
    public class ProductTest
    {
        private readonly Mock<IProductService> _mock;
        private readonly ProductController _controller;

        public ProductTest()
        {
            _mock = new Mock<IProductService>();
            _controller = new ProductController(_mock.Object);
        }

        [Fact]
        public async Task Create_ReturnOkResult()
        {
            var input = new CreateProductInputDto
            {
                Name = "Mouse Gamer",
                SKU = "PRD-001",
                Quantity = 150,
                MinimumQuantity = 20,
                Price = 120
            };

            ApiResponse<object> response = new()
            {
                Status = true,
                Message = "Product successfully registered!",
                Data = null
            };
            CancellationToken cancellationToken = new CancellationToken();

            _mock.Setup(s => s.Create(input, cancellationToken))
                .Returns(Task.CompletedTask);

            var result = await _controller.Create(input, cancellationToken);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse<object>>(okResult.Value);

            Assert.True(apiResponse.Status);
        }
    }
}
