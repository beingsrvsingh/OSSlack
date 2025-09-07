using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Query
{
    public record GetAllConsultationModesQuery : IRequest<Result>
    {
    }
}
