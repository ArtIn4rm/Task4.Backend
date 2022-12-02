using FluentValidation;

namespace Task4.Application.CQRS.RegisteredUsers.Commands.BlockRegisteredUser
{
    public class BlockRegisteredUserCommandValidator : AbstractValidator<BlockRegisteredUserCommand>
    {
        public BlockRegisteredUserCommandValidator()
        {
            RuleForEach(blockUserCommand => blockUserCommand.IdList).NotEqual(Guid.Empty);
        }
    }
}
