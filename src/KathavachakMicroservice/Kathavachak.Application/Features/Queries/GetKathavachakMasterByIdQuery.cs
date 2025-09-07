using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Queries
{
    public class GetKathavachakMasterByIdQuery : IRequest<Result>
    {
        public int Id { get; set; }
        public GetKathavachakMasterByIdQuery(int id) => Id = id;
    }
}
