using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Commands
{
    public record DeleteSubCategoryCommand(int Id) : IRequest<Result>;

}