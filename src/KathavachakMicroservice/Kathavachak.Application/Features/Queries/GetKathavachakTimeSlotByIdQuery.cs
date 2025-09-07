using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Queries
{
    public class GetKathavachakTimeSlotByIdQuery : IRequest<Result>
    {
        public int Id { get; set; }
        public GetKathavachakTimeSlotByIdQuery(int id) => Id = id;
    }
}
