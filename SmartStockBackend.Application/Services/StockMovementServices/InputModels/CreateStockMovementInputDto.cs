using SmartStockBackend.Core.Enums;

namespace SmartStockBackend.Application.Services.StockMovementServices.InputModels
{
    public class CreateStockMovementInputDto
    {
        public int ProductId { get; set; }
        public MovementTypeStockMovementEnum Type { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }
    }
}
