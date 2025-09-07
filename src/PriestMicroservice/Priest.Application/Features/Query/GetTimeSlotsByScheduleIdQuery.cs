using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Query
{
    public class GetTimeSlotsByScheduleIdQuery : IRequest<Result>
    {
        public int ScheduleId { get; set; }
    }

}
