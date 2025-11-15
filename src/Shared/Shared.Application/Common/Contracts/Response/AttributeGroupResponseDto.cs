using System.Text.Json.Serialization;

namespace Shared.Application.Common.Contracts.Response
{
    public class AttributeGroupResponseDto
    {
        [JsonPropertyName("attribute_group_name")]
        public string AttributeGroupName { get; set; } = null!;
        public List<AttributeResponseDto> Attributes { get; set; } = new();
    }
}
