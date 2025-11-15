using Shared.Domain.Entities.Interface;

namespace Shared.Domain.Entities.Base
{
    public class BaseAttributeValue : IBaseAttributeValue
    {
        public int? CatalogAttributeId { get; set; }
        public int? CatalogAttributeValueId { get; set; }
        public string? Value { get; set; }
        public string? AttributeKey { get; set; }
        public string? AttributeLabel { get; set; }
        public int? AttributeDataTypeId { get; set; }
        public int? CatalogAttributeGroupId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CategoryNameSnapshot { get; set; }
        public string? AttributeGroupNameSnapshot { get; set; }
    }
}
