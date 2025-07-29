using MediatR;
using Shared.Utilities.Response;

namespace Review.Application.Features.Queries
{
    public class GetReviewsByProductQuery : IRequest<Result>
    {
        public required int ProductId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
