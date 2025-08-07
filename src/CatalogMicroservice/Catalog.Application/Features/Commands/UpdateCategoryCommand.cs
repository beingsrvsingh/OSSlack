using Catalog.Domain.Entities;
using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Commands
{
    public class UpdateCategoryCommand : IRequest<Result>
    {
        public CategoryMaster Category { get; set; }
        public UpdateCategoryCommand(CategoryMaster category) => Category = category;
    }
}