using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Commands
{
    public class UpdatePriestCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string? DisplayName { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }

}
