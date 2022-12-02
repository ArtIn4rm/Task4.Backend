using FluentValidation;

namespace Task4.Application.CQRS.RegisteredUsers.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(createUserCommand =>
                createUserCommand.Email).NotEmpty().MaximumLength(30).Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            RuleFor(createUserCommand =>
                createUserCommand.Password).NotEmpty();
        }
    }
}
