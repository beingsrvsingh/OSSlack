using FluentValidation;
using Identity.Application.Features.User.Commands.CreateUser;

namespace Identity.Application.Features.User.Commands
{
    public sealed class LoginUserEmailCommandValidator : AbstractValidator<CreateUserEmailCommand>
    {
        public LoginUserEmailCommandValidator()
        {
            RuleFor(x => x.Email).MaximumLength(50);
        }
    }
}
