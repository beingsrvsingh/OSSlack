using MediatR;
using Shared.Utilities.Response;

namespace Pooja.Application.Features.Commands
{
    public class UpdatePoojaCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsHomeAvailable { get; set; }
        public decimal Price { get; set; }
    }

}
