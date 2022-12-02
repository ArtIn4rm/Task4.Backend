using MediatR;

namespace Task4.Application.CQRS.RegisteredUsers.Queries.CheckUserAuth
{
    public class CheckUserAuthQuery : IRequest
    {
        public Guid Id { get; set; }
    }
}
