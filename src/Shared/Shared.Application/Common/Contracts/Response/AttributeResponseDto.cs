using Shared.Domain.Enums;
using System.Text.Json.Serialization;

namespace Shared.Application.Common.Contracts.Response
{
    public class AttributeResponseDto
    {
        [JsonPropertyName("key")]
        public string Key { get; set; } = null!;

        [JsonPropertyName("label")]
        public string Label { get; set; } = null!;

        [JsonPropertyName("value")]
        public string? Value { get; set; }

        [JsonPropertyName("data_type_id")]
        public int DataTypeId { get; set; } = (int)AttributeDataType.String;
    }
}
