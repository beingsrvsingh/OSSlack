using MediatR;
using Shared.Utilities.Response;

namespace Pooja.Application.Features.Queries
{
    public class GetPoojasByPriestQuery : IRequest<Result>
    {
        public int PriestId { get; set; }
    }

}
