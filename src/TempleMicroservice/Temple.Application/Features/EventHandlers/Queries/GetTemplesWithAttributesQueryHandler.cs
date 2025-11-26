using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Query;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Queries
{
    public class GetTemplesWithAttributesQueryHandler : IRequestHandler<GetTemplesBySubcategoryIdQuery, Result>
    {
        private readonly ILoggerService<GetTemplesWithAttributesQueryHandler> logger;
        private readonly ITempleService templeService;

        public GetTemplesWithAttributesQueryHandler(ILoggerService<GetTemplesWithAttributesQueryHandler> logger, ITempleService templeService)
        {
            this.logger = logger;
            this.templeService = templeService;
        }

        public async Task<Result> Handle(GetTemplesBySubcategoryIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await templeService.GetTemplesBySubCategoryIdAsync(int.Parse(request.SubCategoryId));

                if (products == null)
                    return Result.Failure(new FailureResponse("NotFound", "No temples found"));

                return Result.Success(products);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception in GetTemplesWithAttributesQueryHandler handler");
                return Result.Failure(new FailureResponse("Error", "An error occurred while processing the request."));
            }
        }
    }
}
