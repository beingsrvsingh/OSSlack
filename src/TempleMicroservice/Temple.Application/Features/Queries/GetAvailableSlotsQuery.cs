using Temple.Application.Contracts;
using MediatR;
using Shared.Utilities.Response;

namespace Temple.Application.Features.Query
{
    public record GetAvailableSlotsQuery(int EntityId, DateTime Date): IRequest<Result>;
}
