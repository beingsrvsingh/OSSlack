using System.Text.Json.Serialization;
using MediatR;
using Shared.Utilities.Response;

namespace Address.Application.Features.Commands
{
    public class CreateAddressCommand : IRequest<Result>
    {

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("label")]
        public string? Label { get; set; }

        [JsonPropertyName("address")]
        public string AddressLine1 { get; set; } = null!;

        [JsonPropertyName("address1")]
        public string? AddressLine2 { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; } = null!;

        [JsonPropertyName("state")]
        public string State { get; set; } = null!;

        [JsonPropertyName("pincode")]
        public string Pincode { get; set; } = null!;

        [JsonPropertyName("landmark")]
        public string? Landmark { get; set; }

        [JsonPropertyName("phone_number")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("is_default")]
        public bool IsDefault { get; set; } = false;
    }
}