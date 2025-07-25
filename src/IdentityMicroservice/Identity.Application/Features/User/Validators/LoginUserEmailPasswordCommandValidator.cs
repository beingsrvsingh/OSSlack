using FluentValidation;
using Identity.Application.Features.User.Commands.CreateUser;

namespace Identity.Application.Features.User.Commands
{
    public sealed class LoginUserEmailPasswordCommandValidator : AbstractValidator<CreateUserEmailCommand>
    {
        public LoginUserEmailPasswordCommandValidator()
        {
            RuleFor(x => x.Email).MaximumLength(50);
        }
    }
}
