using MediatR;
using Shared.Utilities.Response;

namespace Pooja.Application.Features.Queries
{
    public class GetPoojasByTempleQuery : IRequest<Result>
    {
        public int TempleId { get; set; }
    }

}
