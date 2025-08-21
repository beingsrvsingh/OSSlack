using MediatR;
using Product.Domain.Entities;
using Shared.Utilities.Response;

namespace Product.Application.Features.Commands
{
    public class UpdateProductWithAttributesCommand : IRequest<Result>
    {
        public ProductMaster Product { get; set; } = null!;
        public List<ProductAttributeValue> Attributes { get; set; } = new();
    }

}