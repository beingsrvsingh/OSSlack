using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Query
{
    public class GetPriestByIdQuery : IRequest<Result>
    {
        public int Id { get; set; }
    }

}
