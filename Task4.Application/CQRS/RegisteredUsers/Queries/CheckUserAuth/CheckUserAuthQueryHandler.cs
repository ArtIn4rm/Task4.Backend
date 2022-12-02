using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Task4.Application.Common.Exceptions;
using Task4.Application.CQRS.RegisteredUsers.Queries.GetAllRegisteredUsers;
using Task4.Application.Interfaces;
using Task4.Domain;

namespace Task4.Application.CQRS.RegisteredUsers.Queries.CheckUserAuth
{
    public class CheckUserAuthQueryHandler : IRequestHandler<CheckUserAuthQuery>
    {
        private readonly IRegisteredUserDbContext _dbContext;
        private readonly IMapper _mapper;

        public CheckUserAuthQueryHandler(IRegisteredUserDbContext dbContext,
            IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<Unit> Handle(CheckUserAuthQuery request,
           CancellationToken cancellationToken)
        {
            var registeredUser = await _dbContext.RegisteredUsers
                .FirstOrDefaultAsync(value => value.Id == request.Id);

            if(registeredUser == null || registeredUser.Id != request.Id
                || registeredUser.Status == UserStatus.Blocked)
            {
                throw new InvalidTokenException();
            }
            return Unit.Value;
        }
    }
}
