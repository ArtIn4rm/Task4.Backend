using MediatR;

namespace Task4.Application.CQRS.RegisteredUsers.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<Guid>
    {
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
