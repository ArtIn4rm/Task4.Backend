using MediatR;

namespace Task4.Application.CQRS.RegisteredUsers.Commands.CreateRegisteredUser
{
    public class CreateRegisteredUserCommand : IRequest<Guid>
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
