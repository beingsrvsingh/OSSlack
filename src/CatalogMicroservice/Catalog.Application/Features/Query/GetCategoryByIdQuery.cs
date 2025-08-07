using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Query
{
    public class GetCategoryByIdQuery : IRequest<Result>
    {
        public int Id { get; set; }
        public GetCategoryByIdQuery(int id) => Id = id;
    }
}