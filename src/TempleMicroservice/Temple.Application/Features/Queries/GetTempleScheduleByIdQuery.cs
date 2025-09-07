using MediatR;
using Shared.Utilities.Response;

namespace Temple.Application.Features.Queries
{
    public record GetTempleScheduleByIdQuery(int Id) : IRequest<Result>;

}
