using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Query
{
    public class GetPriestExpertiseQuery : IRequest<Result>
    {
        public int PriestId { get; set; }
    }

}
