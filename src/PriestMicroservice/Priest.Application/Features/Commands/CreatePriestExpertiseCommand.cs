using MediatR;
using Priest.Domain.enums;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Commands
{
    public class CreatePriestExpertiseCommand : IRequest<Result>
    {
        public int PriestId { get; set; }
        public PriestExpertiseType ExpertiseArea { get; set; }
    }
}
