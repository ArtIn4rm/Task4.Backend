using Task4.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task4.Application.Common.Exceptions;
using Task4.Domain;
using MediatR;

namespace Task4.Application.CQRS.RegisteredUsers.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Guid>
    {
        private readonly IRegisteredUserDbContext _dbContext;

        public LoginUserCommandHandler(IRegisteredUserDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var regitesteredUser = await _dbContext.RegisteredUsers.FirstOrDefaultAsync(user =>
                user.Email == request.Email, cancellationToken);

            if (regitesteredUser == null || regitesteredUser.Email != request.Email)
            {
                throw new NotFoundException(nameof(RegisteredUser), request.Email!);
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, regitesteredUser.PasswordHash))
            {
                throw new WrongPasswordException();
            }

            if(regitesteredUser.Status == UserStatus.Blocked)
            {
                throw new UserIsBlockedException(regitesteredUser.Name!);
            }

            regitesteredUser.LastAuthorizationDate = DateTime.Now.ToUniversalTime();
            await _dbContext.SaveChangesAsync(cancellationToken);
            return regitesteredUser.Id;
        }
    }
}
