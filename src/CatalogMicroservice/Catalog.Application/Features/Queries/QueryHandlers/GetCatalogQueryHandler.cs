using Catalog.Application.Services;
using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Queries.QueryHandlers
{
    public class GetCatalogQueryHandler : IRequestHandler<GetCatalogQuery, Result>
    {
        private readonly ICatalogService service;

        public GetCatalogQueryHandler(ICatalogService service)
        {
            this.service = service;
        }
        public async Task<Result> Handle(GetCatalogQuery request, CancellationToken cancellationToken)
        {
            var result = await service.GetAllCatalog();

            if (result == null)
            {
                return await Task.FromResult(Result.Success());
            }

            return await Task.FromResult(Result.Success(result));
        }
    }
}
