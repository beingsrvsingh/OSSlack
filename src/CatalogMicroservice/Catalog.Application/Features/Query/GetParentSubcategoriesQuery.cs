using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Query
{
    public class GetParentSubcategoriesQuery : IRequest<Result> { }
}
