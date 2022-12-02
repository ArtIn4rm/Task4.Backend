
namespace Task4.Application.Common.Exceptions
{
    public class UserIsBlockedException : Exception
    {
        public UserIsBlockedException(string name) 
            :base($"User {name} is blocked"){ }
    }
}
