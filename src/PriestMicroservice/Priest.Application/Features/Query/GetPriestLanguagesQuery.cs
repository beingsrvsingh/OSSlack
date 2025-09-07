using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Query
{
    public class GetPriestLanguagesQuery : IRequest<Result>
    {
        public int PriestId { get; set; }
    }

}
