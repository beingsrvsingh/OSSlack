using MediatR;
using Shared.Utilities.Response;

namespace Review.Application.Features.Queries
{
    public class GetProductReviewSummariesQuery : IRequest<Result>
    {
        public required List<int> Pids { get; set; }
    }
}