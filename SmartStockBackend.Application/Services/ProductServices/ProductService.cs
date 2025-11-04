using Microsoft.EntityFrameworkCore;
using SmartStockBackend.Application.Services.ProductServices.InputModels;
using SmartStockBackend.Application.Services.ProductServices.ViewModels;
using SmartStockBackend.Core.Entities;
using SmartStockBackend.Core.Exceptions.ProductExceptions;
using SmartStockBackend.Infra;

namespace SmartStockBackend.Application.Services.ProductServices
{
    public interface IProductService
    {
        public Task<List<ProductDataViewDto>> GetAll(CancellationToken cancellationToken);
        public Task<Product> GetById(int id, CancellationToken cancellationToken);
        public Task Create(CreateProductInputDto model, CancellationToken cancellationToken);
        public Task Update(int id, UpdateProductInputDto model, CancellationToken cancellationToken);
        public Task Delete(int id, CancellationToken cancellationToken);
    }

    public class ProductService : IProductService
    {
        private readonly SmartStockDbContext _context;

        public ProductService(SmartStockDbContext context)
        {
            _context = context;
        }

        public async Task Create(CreateProductInputDto model, CancellationToken cancellationToken)
        {
            Product product = new()
            {
                Name = model.Name,
                SKU = model.SKU,
                Quantity = model.Quantity,
                MinimumQuantity = model.MinimumQuantity,
                Price = model.Price,
                LastRestockDate = DateTime.Now
            };

            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
                ?? throw new ProductNotFoundByIdException(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ProductDataViewDto>> GetAll(CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Select(p => new ProductDataViewDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    SKU = p.SKU,
                    Quantity = p.Quantity
                }).ToListAsync(cancellationToken);
            return products;
        }

        public async Task<Product> GetById(int id, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
                ?? throw new ProductNotFoundByIdException(id);
            return product;
        }

        public async Task Update(int id, UpdateProductInputDto model, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
                ?? throw new ProductNotFoundByIdException(id);
            product.Name = model.Name;
            product.SKU = model.SKU;
            product.Quantity = model.Quantity;
            product.Price = model.Price;
            product.MinimumQuantity = model.MinimumQuantity;
            product.LastRestockDate = model.LastRestockDate;

            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
