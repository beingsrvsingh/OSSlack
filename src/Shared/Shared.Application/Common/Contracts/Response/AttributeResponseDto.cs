namespace Shared.Application.Common.Contracts.Response
{
    public class AttributeResponseDto
    {
        public string Label { get; set; } = null!;
        public string? Value { get; set; }
        public int? DataTypeId { get; set; }
    }
}
