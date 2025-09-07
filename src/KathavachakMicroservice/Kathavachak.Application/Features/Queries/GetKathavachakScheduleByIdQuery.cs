using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Queries
{
    public class GetKathavachakScheduleByIdQuery : IRequest<Result>
    {
        public int Id { get; set; }
        public GetKathavachakScheduleByIdQuery(int id) => Id = id;
    }
}
