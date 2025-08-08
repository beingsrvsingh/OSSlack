using MediatR;
using Product.Application.Features.Commands;
using Product.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Application.Features.EventHandlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly ILoggerService<UpdateProductCommandHandler> _logger;
        private readonly IProductService _productService;

        public UpdateProductCommandHandler(
            ILoggerService<UpdateProductCommandHandler> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var success = await _productService.UpdateProductAsync(request.Product);
                return success
                    ? Result.Success()
                    : Result.Failure(new FailureResponse("Failed", "Failed to update product"));
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in UpdateProductCommandHandler", ex);
                return Result.Failure(new FailureResponse("Exception", ex.Message));
            }
        }
    }

}