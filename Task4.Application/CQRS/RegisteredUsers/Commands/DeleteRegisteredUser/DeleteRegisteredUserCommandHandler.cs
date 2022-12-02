using MediatR;
using Task4.Application.Common.Exceptions;
using Task4.Application.CQRS.RegisteredUsers.Commands.BlockRegisteredUser;
using Task4.Application.Interfaces;
using Task4.Domain;

namespace Task4.Application.CQRS.RegisteredUsers.Commands.DeleteRegisteredUser
{
    public class DeleteRegisteredUserCommandHandler : IRequestHandler<DeleteRegisteredUserCommand>
    {
        private readonly IRegisteredUserDbContext _dbContext;

        public DeleteRegisteredUserCommandHandler(IRegisteredUserDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteRegisteredUserCommand request, CancellationToken cancellationToken)
        {
            var registeredUsers = _dbContext.RegisteredUsers
                .AsEnumerable()
                .Where(user => IsUserInDeleteList(user.Id, request))
                .ToList();

            if (registeredUsers.Count == 0)
            {
                throw new NotFoundException(nameof(RegisteredUser), request.IdList!);
            }
            foreach (var user in registeredUsers)
            {
                _dbContext.RegisteredUsers.Remove(user);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public bool IsUserInDeleteList(Guid userId, DeleteRegisteredUserCommand request)
        {
            foreach (var id in request.IdList!)
            {
                if (userId == id) return true;
            }
            return false;
        }
    }
}
