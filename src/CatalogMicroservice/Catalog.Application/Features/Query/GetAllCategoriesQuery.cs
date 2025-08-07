using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Query
{
    public class GetAllCategoriesQuery : IRequest<Result> { }
}