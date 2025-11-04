using System.Net;

namespace SmartStockBackend.Core.Exceptions.StockMovementExceptions
{
    public class StockMovementNotFoundByIdException : ApiException
    {
        public StockMovementNotFoundByIdException(int id)
            : base($"Movement with iD {id} not found", HttpStatusCode.Found) { }
    }
}
