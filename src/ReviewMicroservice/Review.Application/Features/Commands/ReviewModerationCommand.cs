using MediatR;
using Review.Domain.Enum;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands
{
    public class ReviewModerationCommand : IRequest<Result>
    {
        public required int ReviewId { get; set; }

        public string ModeratorUserId { get; set; } = string.Empty;

        public ReviewStatus Status { get; set; }

        public string? ModerationComment { get; set; }
    }
}