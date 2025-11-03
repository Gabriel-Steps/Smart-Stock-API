using System.Net;

namespace SmartStockBackend.Core.Exceptions.UserExceptions
{
    public class UserValidationException : ApiException
    {
        public UserValidationException()
            : base("Invalid credentials. Please check your email and password.", HttpStatusCode.BadRequest) { }
    }
}
