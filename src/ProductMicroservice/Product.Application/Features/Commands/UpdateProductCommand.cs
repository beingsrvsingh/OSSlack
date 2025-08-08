using MediatR;
using Product.Domain.Entities;
using Shared.Utilities.Response;

namespace Product.Application.Features.Commands
{
    public class UpdateProductCommand : IRequest<Result>
    {
        public ProductMaster Product { get; }

        public UpdateProductCommand(ProductMaster product)
        {
            Product = product;
        }
    }

}