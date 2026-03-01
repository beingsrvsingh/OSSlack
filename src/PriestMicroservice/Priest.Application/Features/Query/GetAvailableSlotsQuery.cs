using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Query
{
    public record GetAvailableSlotsQuery(int EntityId, DateTime Date): IRequest<Result>;
}
