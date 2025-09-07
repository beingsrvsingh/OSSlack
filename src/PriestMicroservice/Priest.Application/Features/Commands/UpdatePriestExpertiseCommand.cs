using MediatR;
using Priest.Domain.enums;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Commands
{
    public class UpdatePriestExpertiseCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public PriestExpertiseType ExpertiseArea { get; set; }
    }
}
