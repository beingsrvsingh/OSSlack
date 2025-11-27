using MediatR;
using Shared.Utilities.Response;

namespace Astrologer.Application.Features.Query
{
    public record GetAstrologersBySubcategoryIdQuery : IRequest<Result>
    {
        public required String SubCategoryId { get; set; }
        public bool IsSummary = false;
        public int Records { get; set; } = 5;
    }
}