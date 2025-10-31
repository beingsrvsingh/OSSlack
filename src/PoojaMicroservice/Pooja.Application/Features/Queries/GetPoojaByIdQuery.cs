using MediatR;
using Shared.Utilities.Response;

namespace Pooja.Application.Features.Queries
{
    public class GetPoojaByIdQuery : IRequest<Result>
    {
        public int Id { get; set; }
    }

}
