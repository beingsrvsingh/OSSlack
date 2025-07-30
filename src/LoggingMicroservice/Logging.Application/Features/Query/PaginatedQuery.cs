using MediatR;
using Shared.Utilities.Response;

namespace Logging.Application.Features.Query
{
    public class PaginatedQuery : IRequest<Result>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}