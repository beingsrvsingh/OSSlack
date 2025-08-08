using MediatR;
using Product.Domain.Entities;
using Shared.Utilities.Response;

namespace Product.Application.Features.Commands
{
    public class CreateProductCommand : IRequest<Result>
    {
        public ProductMaster Product { get; }

        public CreateProductCommand(ProductMaster product)
        {
            Product = product;
        }
    }

}