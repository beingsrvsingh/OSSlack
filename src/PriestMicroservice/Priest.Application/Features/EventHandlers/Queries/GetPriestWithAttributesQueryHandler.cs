using MediatR;
using Priest.Application.Features.Query;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Queries
{
    public class GetPriestWithAttributesQueryHandler : IRequestHandler<GetPriestsBySubcategoryIdQuery, Result>
    {
        private readonly ILoggerService<GetPriestWithAttributesQueryHandler> logger;
        private readonly IPriestService _priestService;

        public GetPriestWithAttributesQueryHandler(ILoggerService<GetPriestWithAttributesQueryHandler> logger, IPriestService priestService)
        {
            this.logger = logger;
            this._priestService = priestService;
        }

        public async Task<Result> Handle(GetPriestsBySubcategoryIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _priestService.GetPriestsBySubCategoryIdAsync(int.Parse(request.SubCategoryId));

                if (products == null)
                    return Result.Failure(new FailureResponse("NotFound", "No priest found"));

                return Result.Success(products);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception in GetPriestsBySubcategoryIdQuery handler");
                return Result.Failure(new FailureResponse("Error", "An error occurred while processing the request."));
            }
        }
    }
}
