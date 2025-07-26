using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features;

public class DeActivateUserCommand : IRequest<Result>
{
    public string UserId { get; set; } = default!;
    public bool IsActive { get; set; } // true = Activate, false = Deactivate
}
