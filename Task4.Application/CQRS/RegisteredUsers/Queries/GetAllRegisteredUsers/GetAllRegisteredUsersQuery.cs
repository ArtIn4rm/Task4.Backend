using MediatR;

namespace Task4.Application.CQRS.RegisteredUsers.Queries.GetAllRegisteredUsers
{
    public class GetAllRegisteredUsersQuery : IRequest<RegisteredUsersVm>
    {
        public bool IsOnlyActive { get; set; }
    }
}
