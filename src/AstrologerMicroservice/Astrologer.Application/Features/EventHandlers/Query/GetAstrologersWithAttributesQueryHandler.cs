using Astrologer.Application.Features.Query;
using AstrologerMicroservice.Application.Service;
using MediatR;
using Astrologer.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Astrologer.Application.Features.EventHandlers.Queries
{
    public class GetAstrologersWithAttributesQueryHandler : IRequestHandler<GetAstrologersBySubcategoryIdQuery, Result>
    {
        private readonly ILoggerService<GetAstrologersWithAttributesQueryHandler> logger;
        private readonly IAstrologerService _astrologerService;

        public GetAstrologersWithAttributesQueryHandler(ILoggerService<GetAstrologersWithAttributesQueryHandler> logger, IAstrologerService astrologerService)
        {
            this.logger = logger;
            this._astrologerService = astrologerService;
        }

        public async Task<Result> Handle(GetAstrologersBySubcategoryIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _astrologerService.GetAstrologersBySubCategoryIdAsync(int.Parse(request.SubCategoryId));

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
