using MediatR;
using Shared.Utilities.Response;

namespace Review.Application.Features.Queries
{
    public record GetReviewByProductQuery : IRequest<Result>
    {
        public required string ProductId { get; set; }
    }
}
