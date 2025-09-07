using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class UpdateKathavachakScheduleCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
    }
}
