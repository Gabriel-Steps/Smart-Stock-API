namespace SmartStockBackend.Core.Entities
{
    public class StockMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public MovementType Type { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }

        public Product Product { get; set; }
    }

    public enum MovementType
    {
        Entry = 1,
        Exit = 2
    }

}
