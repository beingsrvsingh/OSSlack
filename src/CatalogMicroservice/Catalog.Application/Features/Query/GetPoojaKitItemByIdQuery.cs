using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Query
{
    public record GetPoojaKitItemByIdQuery(int Id) : IRequest<Result>;

}