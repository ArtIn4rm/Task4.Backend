using MediatR;
using AutoMapper;
using Task4.Application.Interfaces;
using Task4.Domain;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Task4.Application.CQRS.RegisteredUsers.Queries.GetAllRegisteredUsers
{
    public class GetAllRegisteredUsersQueryHandler 
        : IRequestHandler<GetAllRegisteredUsersQuery, RegisteredUsersVm>
    {
        private readonly IRegisteredUserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllRegisteredUsersQueryHandler(IRegisteredUserDbContext dbContext, 
            IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<RegisteredUsersVm> Handle(GetAllRegisteredUsersQuery request, 
            CancellationToken cancellationToken)
        {
            var usersQuery = await _dbContext.RegisteredUsers
                .Where(user => !request.IsOnlyActive || user.Status == UserStatus.Active)
                .ProjectTo<RegisteredUserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new RegisteredUsersVm { RegisteredUsers = usersQuery };
        }
    }
}
