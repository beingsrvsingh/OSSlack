namespace Shared.Domain.Entities.Interface
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
        string? AttributeGroupNameSnapshot { get; set; }
        DateTime CreatedAt { get; set; }
    }

}
