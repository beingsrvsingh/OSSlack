using MediatR;
using Product.Application.Features.Commands;
using Product.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Application.Features.EventHandlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
    {
        private readonly ILoggerService<CreateProductCommandHandler> _logger;
        private readonly IProductService _productService;

        public CreateProductCommandHandler(
            ILoggerService<CreateProductCommandHandler> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var success = await _productService.AddProductAsync(request.Product);

                return success
                    ? Result.Success()
                    : Result.Failure(new FailureResponse("Failed", "Failed to create product"));
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in CreateProductCommandHandler", ex);
                return Result.Failure(new FailureResponse("Exception", ex.Message));
            }
        }
    }

}