using FluentValidation;

namespace Identity.Application.Features.User.Commands.CreateUser
{
    public sealed class CreateUserEmailCommandValidator : AbstractValidator<CreateUserEmailCommand>
    {
        public CreateUserEmailCommandValidator()
        {
            RuleFor(x => x.Email).MaximumLength(50);
        }
    }
}
