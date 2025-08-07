using Catalog.Domain.Entities;
using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Commands
{
    public record AddOrUpdateSubCategoryLocalizedTextCommand(SubCategoryLocalizedText LocalizedText) : IRequest<Result>;

}