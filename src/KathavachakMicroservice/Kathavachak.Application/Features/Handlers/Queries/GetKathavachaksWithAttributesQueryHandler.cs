using Kathavachak.Application.Features.Query;
using Kathavachak.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Astrologer.Application.Features.EventHandlers.Queries
{
    public class GetKathavachaksWithAttributesQueryHandler : IRequestHandler<GetKathavachaksBySubcategoryIdQuery, Result>
    {
        private readonly ILoggerService<GetKathavachaksWithAttributesQueryHandler> logger;
        private readonly IKathavachakService _kathavachakService;

        public GetKathavachaksWithAttributesQueryHandler(ILoggerService<GetKathavachaksWithAttributesQueryHandler> logger, IKathavachakService kathavachakService)
        {
            this.logger = logger;
            this._kathavachakService = kathavachakService;
        }

        public async Task<Result> Handle(GetKathavachaksBySubcategoryIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _kathavachakService.GetKathavachaksBySubCategoryIdAsync(int.Parse(request.SubCategoryId));

                if (products == null)
                    return Result.Failure(new FailureResponse("NotFound", "No kathavachak found"));

                return Result.Success(products);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception in GetPoojasBySubCategoryIdAsync handler");
                return Result.Failure(new FailureResponse("Error", "An error occurred while processing the request."));
            }
        }
    }
}
