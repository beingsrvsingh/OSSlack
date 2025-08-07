using Catalog.Domain.Entities;
using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Commands
{
    public record UpdateSubCategoryCommand(SubCategoryMaster SubCategory) : IRequest<Result>;

}