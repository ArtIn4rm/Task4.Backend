using MediatR;
using Task4.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task4.Application.Common.Exceptions;
using Task4.Domain;
using AutoMapper;

namespace Task4.Application.CQRS.RegisteredUsers.Commands.BlockRegisteredUser
{
    public class BlockRegisteredUserCommandHandler : IRequestHandler<BlockRegisteredUserCommand>
    {
        private readonly IRegisteredUserDbContext _dbContext;

        private readonly IMapper _mapper;

        public BlockRegisteredUserCommandHandler(IRegisteredUserDbContext dbContext,
            IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<Unit> Handle(BlockRegisteredUserCommand request, CancellationToken cancellationToken)
        {
            var registeredUsers = _dbContext.RegisteredUsers
                .AsEnumerable()
                .Where(user => IsUserInBlockList(user.Id, request))
                .ToList();

            if (registeredUsers.Count == 0)
            {
                throw new NotFoundException(nameof(RegisteredUser), request.IdList!);
            }

            foreach(var user in registeredUsers)
            {
                if (request.IsBlocking)
                {
                    user.Status = UserStatus.Blocked;
                }
                else
                {
                    user.Status = UserStatus.Active;
                }
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public bool IsUserInBlockList(Guid userId, BlockRegisteredUserCommand request)
        {
            foreach (var id in request.IdList!)
            {
                if (userId == id) return true;
            }
            return false;
        }
    }
}
