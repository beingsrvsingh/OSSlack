using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Commands
{
    public class CreateScheduleCommand : IRequest<Result>
    {
        public int PriestId { get; set; }
        public DayOfWeek Day { get; set; }
        public DateTime Date { get; set; }
    }
}
