namespace SmartStockBackend.Core.Entities
{
    public class DemandPrediction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime PredictionDate { get; set; }
        public int PredictedDaysToDepletion { get; set; }

        public Product Product { get; set; }
    }
}
