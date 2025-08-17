using MediatR;
using Shared.Utilities.Response;

namespace Product.Application.Features.Query
{
    public record GetProductByProductName : IRequest<Result>
    {
        public required string ProductName { get; set; }
    }
}