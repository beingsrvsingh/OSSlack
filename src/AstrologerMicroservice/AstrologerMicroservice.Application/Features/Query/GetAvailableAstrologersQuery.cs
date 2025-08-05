using AstrologerMicroservice.Domain.Entities.Enums;
using MediatR;
using Shared.Utilities.Response;

namespace AstrologerMicroservice.Application.Features.Query
{
    public record GetAvailableAstrologersQuery(DateTime Date, Languages Language, ExpertiseType Expertise) : IRequest<Result> { }

}