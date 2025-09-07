using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Commands
{
    public class CreatePriestCommand : IRequest<Result>
    {
        public string UserId { get; set; } = null!;
        public string? DisplayName { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }

}
