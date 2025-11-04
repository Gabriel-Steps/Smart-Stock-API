using System.Net;

namespace SmartStockBackend.Core.Exceptions.ProductExceptions
{
    public class ProductNotFoundByIdException : ApiException
    {
        public ProductNotFoundByIdException(int id)
            : base($"Product with iD {id} not found", HttpStatusCode.NotFound) { }
    }
}
