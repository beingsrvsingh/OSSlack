using MediatR;
using Shared.Utilities.Response;

namespace Product.Application.Features.Commands
{
    public class SeedCatalogCommand : IRequest<Result>{};
}