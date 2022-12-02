using MediatR;

namespace Task4.Application.CQRS.RegisteredUsers.Commands.DeleteRegisteredUser
{
    public class DeleteRegisteredUserCommand : IRequest
    {
        public Guid[]? IdList { get; set; }
    }
}
