using MediatR;
using Shared.Utilities.Response;
using System.ComponentModel.DataAnnotations;

namespace Priest.Application.Features.Commands
{
    public class UpdateTimeSlotCommand : IRequest<Result>
    {
        public int Id { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }
        public bool IsBooked { get; set; }
    }

}
