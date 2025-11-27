using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Query
{
    public record GetKathavachaksBySubcategoryIdQuery : IRequest<Result>
    {
        public required String SubCategoryId { get; set; }
        public bool IsSummary = false;
        public int Records { get; set; } = 5;
    }
}