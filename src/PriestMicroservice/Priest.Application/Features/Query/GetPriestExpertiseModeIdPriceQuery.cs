using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Query
{
    public record class GetPriestExpertiseModeIdPriceQuery : IRequest<Result>
    {
        public required int PriestExpertiseId { get; set; }
        public required int ModeId { get; set; }
    }
}
