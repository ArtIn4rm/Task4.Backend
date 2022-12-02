
namespace Task4.Application.Common.Exceptions
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException()
            : base("Incorrect password") { }
    }
}
