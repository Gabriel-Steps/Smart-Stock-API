namespace SmartStockBackend.Core.Exceptions.UserExceptions
{
    public class UserNotFoundException : ApiException
    {
        public UserNotFoundException(string email) 
            : base($"User with e-mail {email} not found") { }
    }
}
