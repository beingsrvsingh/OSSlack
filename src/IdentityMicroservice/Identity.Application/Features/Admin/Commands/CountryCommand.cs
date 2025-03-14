using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands
{
    public class CountryCommand : IRequest<Result>
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Dial_Code { get; set; } = null!;
        public string Emoji { get; set; } = null!;
        public string Unicode { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}
