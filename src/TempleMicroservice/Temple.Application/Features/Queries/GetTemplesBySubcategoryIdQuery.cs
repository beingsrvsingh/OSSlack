using MediatR;
using Shared.Utilities.Response;

namespace Temple.Application.Features.Query
{
    public record GetTemplesBySubcategoryIdQuery : IRequest<Result>
    {
        public required String SubCategoryId { get; set; }
        public bool IsSummary = false;
        public int Records { get; set; } = 5;
    }
}