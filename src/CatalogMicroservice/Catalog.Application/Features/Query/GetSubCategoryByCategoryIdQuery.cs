using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Query
{
    public class GetSubCategoryByCategoryIdQuery : IRequest<Result>
    {
        public int Id { get; set; }
        public GetSubCategoryByCategoryIdQuery(int id) => Id = id;
    }
}