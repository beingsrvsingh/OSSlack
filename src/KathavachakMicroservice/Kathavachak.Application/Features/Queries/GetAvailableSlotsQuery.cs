using Kathavachak.Application.Contracts;
using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Query
{
    public record GetAvailableSlotsQuery(int EntityId, DateTime Date): IRequest<Result>;
}
