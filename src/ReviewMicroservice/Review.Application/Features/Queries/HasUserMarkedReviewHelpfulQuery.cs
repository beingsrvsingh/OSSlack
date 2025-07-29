using MediatR;
using Shared.Utilities.Response;

namespace Review.Application.Features.Queries
{
    public class HasUserMarkedReviewHelpfulQuery : IRequest<Result>
    {
        public int ReviewId { get; set; }
        public string UserId { get; set; } = null!;
    }

}