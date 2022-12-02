using MediatR;
using Task4.Domain;
using Task4.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task4.Application.Common.Exceptions;

namespace Task4.Application.CQRS.RegisteredUsers.Commands.CreateRegisteredUser
{
    public class CreateRegisteredUserCommandHandler : IRequestHandler<CreateRegisteredUserCommand, Guid>
    {
        private readonly IRegisteredUserDbContext _dbContext;

        public CreateRegisteredUserCommandHandler(IRegisteredUserDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateRegisteredUserCommand request, CancellationToken cancellationToken)
        {
            var registeredUser = new RegisteredUser
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RegistrationDate = DateTime.Now,
                LastAuthorizationDate = DateTime.Now,
                Status = UserStatus.Active,
            };
            var similar = await _dbContext.RegisteredUsers
                .FirstOrDefaultAsync(value => value.Email == request.Email || 
                    value.Name == request.Name, cancellationToken);

            Console.WriteLine(BCrypt.Net.BCrypt.HashPassword(request.Password).Length);

            if (similar != null)
            {
                throw new HasTakenException((similar.Email == request.Email) ? "Email" : "Username"); 
            }

            await _dbContext.RegisteredUsers.AddAsync(registeredUser, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return registeredUser.Id;
        }
    }
}
