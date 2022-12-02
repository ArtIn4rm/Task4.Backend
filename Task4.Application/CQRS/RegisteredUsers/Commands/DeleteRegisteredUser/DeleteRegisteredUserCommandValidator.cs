using FluentValidation;

namespace Task4.Application.CQRS.RegisteredUsers.Commands.DeleteRegisteredUser
{
    public class DeleteRegisteredUserCommandValidator : AbstractValidator<DeleteRegisteredUserCommand>
    {
        public DeleteRegisteredUserCommandValidator()
        {
            RuleForEach(deleteUserCommand => deleteUserCommand.IdList).NotEqual(Guid.Empty);
        }
    }
}
