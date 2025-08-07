using Catalog.Domain.Entities;
using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Commands
{
    public record CreateSubCategoryCommand(SubCategoryMaster SubCategory) : IRequest<Result>;

}