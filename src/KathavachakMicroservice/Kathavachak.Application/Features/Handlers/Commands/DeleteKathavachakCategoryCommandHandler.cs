using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class DeleteKathavachakCategoryCommandHandler : IRequestHandler<DeleteKathavachakCategoryCommand, Result>
    {
        private readonly IKathavachakCategoryService _service;
        private readonly ILoggerService<DeleteKathavachakCategoryCommandHandler> _logger;

        public DeleteKathavachakCategoryCommandHandler(
            IKathavachakCategoryService service,
            ILoggerService<DeleteKathavachakCategoryCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteKathavachakCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.DeleteAsync(request.Id);
                return result ? Result.Success("Category deleted.") : Result.Failure(new FailureResponse("DELETE_FAILED", "Delete failed."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteKathavachakCategory: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", ex.Message));
            }
        }
    }

}
