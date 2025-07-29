using MediatR;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands
{
    public class DeleteReviewCommand : IRequest<Result>
    {
        public int ReviewId { get; set; }
        public required string RequestingUserId { get; set; }
    }
}