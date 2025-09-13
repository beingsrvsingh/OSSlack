namespace Shared.Domain.Entities.Base
{
    public interface IBaseAttributeValue
    {
        int? CatalogAttributeId { get; set; }
        int? CatalogAttributeValueId { get; set; }
        string? Value { get; set; }
        string? AttributeKey { get; set; }
        string? AttributeLabel { get; set; }
        int? AttributeDataTypeId { get; set; }
        int? CatalogAttributeGroupId { get; set; }
        DateTime CreatedAt { get; set; }
        string? CategoryNameSnapshot { get; set; }
    }

}
