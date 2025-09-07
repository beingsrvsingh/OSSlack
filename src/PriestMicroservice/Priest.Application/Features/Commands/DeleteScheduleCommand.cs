using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Commands
{
    public class DeleteScheduleCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
