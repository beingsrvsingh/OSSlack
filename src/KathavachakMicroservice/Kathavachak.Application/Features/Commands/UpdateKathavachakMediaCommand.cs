using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class UpdateKathavachakMediaCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? MediaType { get; set; }
    }

}
