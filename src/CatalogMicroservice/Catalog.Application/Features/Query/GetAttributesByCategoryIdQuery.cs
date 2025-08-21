using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Query
{
    public class GetAttributesByCategoryIdQuery : IRequest<Result>
    {
        public int CategoryId { get; }

        public GetAttributesByCategoryIdQuery(int categoryId)
        {
            CategoryId = categoryId;
        }
    }

}