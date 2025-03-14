using FluentValidation;

namespace Identity.Application.Features.User.Commands.Token
{
    public class RevokeUserCommandValidator : AbstractValidator<RevokeUserCommand>
    {
        public RevokeUserCommandValidator()
        {
            RuleFor(x => x.Token).NotEmpty().WithMessage("Token is required");
        }
    }
}
