using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Response;

namespace Product.Application.Features.Query
{
    public class GetProductQuery : IRequest<Result>
    {        
        public int productId { get; init; }
        public bool IsSummary { get; init; } = false;
    }
}