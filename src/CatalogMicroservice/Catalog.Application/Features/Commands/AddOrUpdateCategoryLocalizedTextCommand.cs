using Catalog.Domain.Entities;
using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Commands
{
    public class AddOrUpdateCategoryLocalizedTextCommand : IRequest<Result>
    {
        public CategoryLocalizedText LocalizedText { get; set; }
        public AddOrUpdateCategoryLocalizedTextCommand(CategoryLocalizedText text) => LocalizedText = text;
    }

}