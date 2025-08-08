using MediatR;
using Product.Application.Features.Commands;
using Product.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Application.Features.EventHandlers.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly ILoggerService<DeleteProductCommandHandler> _logger;
        private readonly IProductService _productService;

        public DeleteProductCommandHandler(
            ILoggerService<DeleteProductCommandHandler> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var success = await _productService.DeleteProductAsync(request.ProductId);
                return success
                    ? Result.Success()
                    : Result.Failure(new FailureResponse("Failed", "Failed to delete product"));
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in DeleteProductCommandHandler", ex);
                return Result.Failure(new FailureResponse("Exception", ex.Message));
            }
        }
    }

}