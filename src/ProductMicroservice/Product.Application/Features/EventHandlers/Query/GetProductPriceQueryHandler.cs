using MediatR;
using Product.Application.Features.Query;
using Product.Domain.Repository;
using Shared.Utilities.Response;

namespace Product.Application.Features.EventHandlers.Query
{
    public class GetProductPriceQueryHandler : IRequestHandler<GetProductPriceQuery, Result>
    {
        private readonly IProductRepository _productRepository;

        public GetProductPriceQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(GetProductPriceQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetPriceAsync(request.ProductId);

            return Result.Success(product);
        }
    }

}
