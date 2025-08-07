using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Commands
{
    public class DeleteCategoryCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public DeleteCategoryCommand(int id) => Id = id;
    }

}