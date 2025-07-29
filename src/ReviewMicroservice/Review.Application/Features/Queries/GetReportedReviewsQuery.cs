using MediatR;
using Shared.Utilities.Response;

namespace Review.Application.Features.Queries
{
    public class GetReportedReviewsQuery : IRequest<Result>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}