using MediatR;
using Pooja.Application.Features.Query;
using Pooja.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Pooja.Application.Features.EventHandlers.Queries
{
    public class GetPoojasWithAttributesQueryHandler : IRequestHandler<GetPoojasBySubcategoryIdQuery, Result>
    {
        private readonly ILoggerService<GetPoojasWithAttributesQueryHandler> logger;
        private readonly IPoojaService _poojaService;

        public GetPoojasWithAttributesQueryHandler(ILoggerService<GetPoojasWithAttributesQueryHandler> logger, IPoojaService poojaService)
        {
            this.logger = logger;
            this._poojaService = poojaService;
        }

        public async Task<Result> Handle(GetPoojasBySubcategoryIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _poojaService.GetPoojasBySubCategoryIdAsync(int.Parse(request.SubCategoryId));

                if (products == null)
                    return Result.Failure(new FailureResponse("NotFound", "No pooja found"));

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
