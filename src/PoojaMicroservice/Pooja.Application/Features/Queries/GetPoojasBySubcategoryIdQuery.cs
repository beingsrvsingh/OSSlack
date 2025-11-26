using MediatR;
using Shared.Utilities.Response;

namespace Pooja.Application.Features.Query
{
    public record GetPoojasBySubcategoryIdQuery : IRequest<Result>
    {
        public required String SubCategoryId { get; set; }
        public bool IsSummary = false;
        public int Records { get; set; } = 5;
    }
}