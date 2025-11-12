using Microsoft.EntityFrameworkCore;
using SmartStockBackend.Application.Services.DemandPredictionServices.ViewModels;
using SmartStockBackend.Core.Entities;
using SmartStockBackend.Core.Enums;
using SmartStockBackend.Core.Exceptions.ProductExceptions;
using SmartStockBackend.Infra;

namespace SmartStockBackend.Application.Services.DemandPredictionServices
{
    public interface IDemandPredictionService
    {
        Task<DemandPredictionProductViewDto> DemandPredictionProduct(int productId, CancellationToken cancellationToken);
    }
    public class DemandPredictionService : IDemandPredictionService
    {
        private readonly SmartStockDbContext _context;

        public DemandPredictionService(SmartStockDbContext context)
        {
            _context = context;
        }

        public async Task<DemandPredictionProductViewDto> DemandPredictionProduct(int productId, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken)
                ?? throw new ProductNotFoundByIdException(productId);

            var today = DateTime.Now;
            var thirtyDaysAgo = today.AddDays(-30);

            var recentMovements = await _context.StockMovements
                .Where(m => m.ProductId == productId &&
                            m.Type == MovementTypeStockMovementEnum.Exit &&
                            m.MovementDate >= thirtyDaysAgo)
                .ToListAsync(cancellationToken);

            float predictedDays = 0;

            if (recentMovements.Any())
            {
                var totalOut = recentMovements.Sum(m => m.Quantity);
                var totalDays = (recentMovements.Max(m => m.MovementDate) - recentMovements.Min(m => m.MovementDate)).TotalDays;
                if (totalDays <= 0)
                    totalDays = 1;

                var avgDailySales = (float)(totalOut / totalDays);

                predictedDays = avgDailySales > 0
                    ? (float)Math.Round((product.Quantity - product.MinimumQuantity) / avgDailySales, 2)
                    : 0;
            }

            var demandPrediction = await _context.DemandPredictions
                .FirstOrDefaultAsync(d => d.ProductId == productId, cancellationToken);

            if (demandPrediction != null)
            {
                demandPrediction.PredictionDate = today;
                demandPrediction.PredictedDaysToDepletion = (int)Math.Max(predictedDays, 0);
                _context.DemandPredictions.Update(demandPrediction);
            }
            else
            {
                await _context.DemandPredictions.AddAsync(new DemandPrediction
                {
                    ProductId = product.Id,
                    PredictionDate = today,
                    PredictedDaysToDepletion = (int)Math.Max(predictedDays, 0)
                }, cancellationToken);
            }
            await _context.SaveChangesAsync(cancellationToken);

            return new DemandPredictionProductViewDto
            {
                PredictionDays = (float)Math.Round(predictedDays, 2),
            };
        }
    }
}