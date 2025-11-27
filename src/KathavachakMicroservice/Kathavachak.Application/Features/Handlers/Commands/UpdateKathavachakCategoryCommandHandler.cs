using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class UpdateKathavachakCategoryCommandHandler : IRequestHandler<UpdateKathavachakCategoryCommand, Result>
    {
        private readonly IKathavachakCategoryService _service;
        private readonly ILoggerService<UpdateKathavachakCategoryCommandHandler> _logger;

        public UpdateKathavachakCategoryCommandHandler(
            IKathavachakCategoryService service,
            ILoggerService<UpdateKathavachakCategoryCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateKathavachakCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _service.GetByIdAsync(request.Id);
                if (existing == null) return Result.Failure(new FailureResponse("NOT_FOUND", "Category entry not found."));

                request.Adapt(existing);
                var result = await _service.UpdateAsync(existing);
                return result ? Result.Success("Category updated.") : Result.Failure(new FailureResponse("UPDATE_FAILED", "Update failed."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateKathavachakCategory: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", ex.Message));
            }
        }
    }

}
