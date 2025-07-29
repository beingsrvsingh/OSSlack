using MediatR;
using Shared.Utilities.Response;

namespace Review.Application.Features.Queries
{
    public class GetReviewsByUserQuery : IRequest<Result>
{
    public required string UserId { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
}