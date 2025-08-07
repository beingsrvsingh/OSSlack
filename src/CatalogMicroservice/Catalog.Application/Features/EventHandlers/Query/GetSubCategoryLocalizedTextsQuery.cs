using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public record GetSubCategoryLocalizedTextsQuery(int SubCategoryId) : IRequest<Result>;

}