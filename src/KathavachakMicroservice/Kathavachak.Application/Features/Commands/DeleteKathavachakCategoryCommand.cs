using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class DeleteKathavachakCategoryCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public DeleteKathavachakCategoryCommand(int id) => Id = id;
    }

}
