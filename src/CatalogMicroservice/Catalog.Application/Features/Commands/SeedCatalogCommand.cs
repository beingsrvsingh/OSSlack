using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Commands
{
    public class SeedCatalogCommand : IRequest<Result>{};
}