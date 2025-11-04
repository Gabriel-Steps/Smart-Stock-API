using System.Net;

namespace SmartStockBackend.Core.Exceptions.StockMovementExceptions
{
    public class MinimumProductQuantityHasBeenExceededException : ApiException
    {
        public MinimumProductQuantityHasBeenExceededException(string nameProduct) 
            : base($"The minimum quantity of the {nameProduct} product has been exceeded.", HttpStatusCode.BadRequest)
        {
        }
    }
}
