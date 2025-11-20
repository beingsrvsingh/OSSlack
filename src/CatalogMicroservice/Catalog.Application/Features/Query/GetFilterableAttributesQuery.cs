using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Query
{
    public record GetFilterableAttributesQuery(int Scid, bool IsFilterable = true) : IRequest<Result>
    {
        
    }
}