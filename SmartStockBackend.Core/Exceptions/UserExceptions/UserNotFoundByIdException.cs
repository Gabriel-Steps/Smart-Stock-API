namespace SmartStockBackend.Core.Exceptions.UserExceptions
{
    public class UserNotFoundByIdException : ApiException
    {
        public UserNotFoundByIdException(int id) 
            : base($"User with ID {id} not found") { }
    }
}
