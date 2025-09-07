using MediatR;
using Shared.Utilities.Response;
using System.ComponentModel.DataAnnotations;

namespace Priest.Application.Features.Commands
{
    public class CreateTimeSlotCommand : IRequest<Result>
    {
        public int ScheduleId { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }
        public bool IsBooked { get; set; } = false;
    }
}
