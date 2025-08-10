using AstrologerMicroservice.Domain.Entities.Enums;
using MediatR;
using Shared.Utilities.Response;

namespace AstrologerMicroservice.Application.Features.Commands
{
    public class UpdateAstrologerCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public List<Languages> Languages { get; set; } = new();
        public List<ExpertiseType> Expertise { get; set; } = new();
        public string Bio { get; set; } = string.Empty;
        public List<DayOfWeek> AvailableDays { get; set; } = new();
        public bool OffersKundli { get; set; }
        public bool OffersConsultation { get; set; }
        public bool OffersPooja { get; set; }
    }
}