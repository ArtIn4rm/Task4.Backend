using FluentValidation;

namespace Task4.Application.CQRS.RegisteredUsers.Commands.CreateRegisteredUser
{
    public class CreateRegisteredUserCommandValidator : AbstractValidator<CreateRegisteredUserCommand>
    {
        public CreateRegisteredUserCommandValidator()
        {
            RuleFor(createUserCommand =>
                createUserCommand.Name).NotEmpty().MaximumLength(40).Matches(@"[A-Za-z]([A-Za-z\-|_0-9])*");
            RuleFor(createUserCommand =>
                createUserCommand.Email).NotEmpty().MaximumLength(30).Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            RuleFor(createUserCommand =>
                createUserCommand.Password).NotEmpty();
        }
    }
}
