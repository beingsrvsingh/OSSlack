using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Commands
{
    public class UpdateScheduleCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public DateTime Date { get; set; }
    }
}
