using System.Text.Json.Serialization;
using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands
{
    public class CountryCommand : IRequest<Result>
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("code")]
        public string Code { get; set; } = null!;

        [JsonPropertyName("dial_code")]
        public string Dial_Code { get; set; } = null!;

        [JsonPropertyName("emoji")]
        public string Emoji { get; set; } = null!;

        [JsonPropertyName("unicode")]
        public string Unicode { get; set; } = null!;

        [JsonPropertyName("image")]
        public string Image { get; set; } = null!;
    }
}
