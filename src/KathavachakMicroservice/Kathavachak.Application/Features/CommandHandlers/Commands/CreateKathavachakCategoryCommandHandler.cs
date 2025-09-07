using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using Kathavachak.Domain.Entities;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class CreateKathavachakCategoryCommandHandler : IRequestHandler<CreateKathavachakCategoryCommand, Result>
    {
        private readonly IKathavachakCategoryService _service;
        private readonly ILoggerService<CreateKathavachakCategoryCommandHandler> _logger;

        public CreateKathavachakCategoryCommandHandler(
            IKathavachakCategoryService service,
            ILoggerService<CreateKathavachakCategoryCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateKathavachakCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = request.Adapt<KathavachakCategory>();
                var success = await _service.CreateAsync(entity);
                return success ? Result.Success("Category added.") : Result.Failure(new FailureResponse("CREATE_FAILED", "Unable to add category."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateKathavachakCategory: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", ex.Message));
            }
        }
    }

}
