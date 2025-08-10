using Temple.Domain.Entities.Enums;
using MediatR;
using Shared.Utilities.Response;

namespace Temple.Application.Features.Commands
{
    public class CreateTempleCommand : IRequest<Result>
    {
        public required string UserId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public List<Languages> Languages { get; set; } = new();
        public List<ExpertiseType> Expertise { get; set; } = new();
        public string Bio { get; set; } = string.Empty;
        public List<DayOfWeek> AvailableDays { get; set; } = new();
        public bool OffersKundli { get; set; }
        public bool OffersConsultation { get; set; }
        public bool OffersPooja { get; set; }
    }
}