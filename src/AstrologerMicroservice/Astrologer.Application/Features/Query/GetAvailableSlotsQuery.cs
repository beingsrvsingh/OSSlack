using Astrologer.Application.Contracts;
using MediatR;
using Shared.Utilities.Response;

namespace Astrologer.Application.Features.Query
{
    public record GetAvailableSlotsQuery(int AstrologerId, DateTime Date): IRequest<Result>;
}
