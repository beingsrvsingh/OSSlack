using Catalog.Domain.Entities;
using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Commands
{
    public class CreateCategoryCommand : IRequest<Result>
    {
        public CategoryMaster Category { get; set; }
        public CreateCategoryCommand(CategoryMaster category) => Category = category;
    }

}