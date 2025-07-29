using MediatR;
using Shared.Utilities.Response;

namespace Review.Application.Features.Queries
{
    public class GetReviewByIdQuery : IRequest<Result>
    {
        public required int ReviewId { get; set; }
    }

}