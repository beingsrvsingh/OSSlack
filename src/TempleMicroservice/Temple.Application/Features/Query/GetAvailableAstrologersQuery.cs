using Temple.Domain.Entities.Enums;
using MediatR;
using Shared.Utilities.Response;

namespace Temple.Application.Features.Query
{
    public record GetAvailableAstrologersQuery(DateTime Date, Languages Language, ExpertiseType Expertise) : IRequest<Result> { }

}