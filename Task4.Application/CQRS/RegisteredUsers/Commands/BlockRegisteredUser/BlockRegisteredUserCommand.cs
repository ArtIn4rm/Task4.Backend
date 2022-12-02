using MediatR;

namespace Task4.Application.CQRS.RegisteredUsers.Commands.BlockRegisteredUser
{
    public class BlockRegisteredUserCommand : IRequest
    {
        public Guid[]? IdList { get; set; }
        
        public bool IsBlocking { get; set; }
    }
}
