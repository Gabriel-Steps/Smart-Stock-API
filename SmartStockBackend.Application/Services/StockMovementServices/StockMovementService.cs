using Microsoft.EntityFrameworkCore;
using SmartStockBackend.Application.Services.StockMovementServices.InputModels;
using SmartStockBackend.Application.Services.StockMovementServices.ViewModels;
using SmartStockBackend.Core.Entities;
using SmartStockBackend.Core.Enums;
using SmartStockBackend.Core.Exceptions.ProductExceptions;
using SmartStockBackend.Core.Exceptions.StockMovementExceptions;
using SmartStockBackend.Infra;

namespace SmartStockBackend.Application.Services.StockMovementServices
{
    public interface IStockMovementService
    {
        public Task<List<StockMovementDataViewDto>> GetAll(int id, CancellationToken cancellationToken);
        public Task Create(CreateStockMovementInputDto model, CancellationToken cancellationToken);
        public Task Delete(int id, CancellationToken cancellationToken);
    }
    public class StockMovementService : IStockMovementService
    {
        private readonly SmartStockDbContext _context;

        public StockMovementService(SmartStockDbContext context)
        {
            _context = context;
        }

        public async Task Create(CreateStockMovementInputDto model, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.ProductId, cancellationToken)
                ?? throw new ProductNotFoundByIdException(model.ProductId);
            if (product.MinimumQuantity > (product.Quantity - model.Quantity))
                throw new MinimumProductQuantityHasBeenExceededException(product.Name);

            StockMovement stockMovement = new()
            {
                ProductId = model.ProductId,
                Type = model.Type,
                Quantity = model.Quantity,
                MovementDate = DateTime.Now
            };

            if (model.Type == MovementTypeStockMovementEnum.Entry) 
                product.Quantity = product.Quantity + model.Quantity;
            else if(model.Type == MovementTypeStockMovementEnum.Exit)
                product.Quantity = product.Quantity - model.Quantity;

            await _context.StockMovements.AddAsync(stockMovement, cancellationToken);
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var stockMovement = await _context.StockMovements.FirstOrDefaultAsync(sm => sm.Id == id, cancellationToken)
                ?? throw new StockMovementNotFoundByIdException(id);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == stockMovement.ProductId, cancellationToken)
                ?? throw new ProductNotFoundByIdException(id);

            if (stockMovement.Type == MovementTypeStockMovementEnum.Entry)
                product.Quantity = product.Quantity - stockMovement.Quantity;
            else if (stockMovement.Type == MovementTypeStockMovementEnum.Exit)
                product.Quantity = product.Quantity + stockMovement.Quantity;

            _context.StockMovements.Remove(stockMovement);
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<StockMovementDataViewDto>> GetAll(int id, CancellationToken cancellationToken)
        {
            var movements = await _context.StockMovements
                .Where(sm => sm.ProductId == id)
                .Select(sm => new StockMovementDataViewDto()
                {
                    Id = sm.Id,
                    Type = sm.Type,
                    Quantity = sm.Quantity,
                    MovementDate = sm.MovementDate
                }).ToListAsync(cancellationToken);
            return movements;
        }
    }
}
