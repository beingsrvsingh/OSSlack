using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Query
{
    public class GetCategoryLocalizedTextsQuery : IRequest<Result>
    {
        public int CategoryId { get; set; }
        public GetCategoryLocalizedTextsQuery(int categoryId) => CategoryId = categoryId;
    }

}