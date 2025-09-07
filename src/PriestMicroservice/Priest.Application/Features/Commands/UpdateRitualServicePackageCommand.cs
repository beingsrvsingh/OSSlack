using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Commands
{
    public class UpdateRitualServicePackageCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
