using SmartStockBackend.Core.Enums;

namespace SmartStockBackend.Application.Services.StockMovementServices.ViewModels
{
    public class StockMovementDataViewDto
    {
        public int Id { get; set; }
        public MovementTypeStockMovementEnum Type { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }
    }
}
