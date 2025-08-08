using MediatR;
using Shared.Utilities.Response;

namespace Product.Application.Features.Commands
{
    public class DeleteProductCommand : IRequest<Result>
    {
        public int ProductId { get; }

        public DeleteProductCommand(int productId)
        {
            ProductId = productId;
        }
    }

}