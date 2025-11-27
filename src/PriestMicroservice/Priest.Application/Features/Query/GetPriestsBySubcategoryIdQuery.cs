using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Query
{
    public record GetPriestsBySubcategoryIdQuery : IRequest<Result>
    {
        public required String SubCategoryId { get; set; }
        public bool IsSummary = false;
        public int Records { get; set; } = 5;
    }
}