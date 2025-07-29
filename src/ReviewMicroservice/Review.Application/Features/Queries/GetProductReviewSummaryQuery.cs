using MediatR;
using Shared.Utilities.Response;

namespace Review.Application.Features.Queries
{
    public class GetProductReviewSummaryQuery : IRequest<Result>
    {
        public int ProductId { get; set; }
    }
}