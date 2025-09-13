using Shared.Application.Common.Contracts;
using System.Text.Json.Serialization;

namespace SearchAggregator.Application.Contracts.Dtos
{
    public class ProductBySubCategoryResponseDto : BaseProductResponseDto
    {
        //[JsonPropertyName("attributes")]
        //public List<BaseProductAttributeDto>? Attributes { get; set; }

        //public static List<ProductBySubCategoryResponseDto> FromEntityList(IEnumerable<AggregatedSearchResultDto> products, IEnumerable<CatalogAttributeDto> catalogAttributeGroups, bool isSummary = false)
        //{
        //    return products.Select(p => FromEntity(p, catalogAttributeGroups, isSummary)).ToList();
        //}

        //public static ProductBySubCategoryResponseDto FromEntity(
        //AggregatedSearchResultDto entity,
        //IEnumerable<CatalogAttributeDto> catalogAttributes,
        //bool isSummary = false)
        //{
        //    // Build dictionary from attribute definitions keyed by attribute key
        //    var attributeDict = catalogAttributes
        //        .ToDictionary(attr => attr.Key, attr => attr);

        //    var attributeValues = entity.AttributeValues ?? new List<BaseProductAttributeValue>();

        //    // If not summary view, only include values with matching definitions
        //    if (!isSummary)
        //    {
        //        attributeValues = attributeValues
        //            .Where(attrVal => attrVal.AttributeKey != null && attributeDict.ContainsKey(attrVal.AttributeKey))
        //            .ToList();
        //    }

        //    // Group values by attribute key
        //    var groupedAttributeValues = attributeValues
        //        .Where(val => !string.IsNullOrEmpty(val.AttributeKey))
        //        .GroupBy(val => val.AttributeKey!)
        //        .ToList();

        //    // Build ProductAttributeDto list
        //    var attributes = groupedAttributeValues.Select(group =>
        //    {
        //        var firstVal = group.First();
        //        attributeDict.TryGetValue(group.Key, out var definition);

        //        return new ProductAttributeDto
        //        {
        //            Key = firstVal.AttributeKey!,
        //            Label = firstVal.AttributeLabel ?? firstVal.AttributeKey!,
        //            Values = group.Select(v => v.Value).Where(v => !string.IsNullOrWhiteSpace(v)).ToList(),
        //            DataType = definition?.DataType ?? "String",
        //            Icon = definition?.Icon,
        //            AllowedValues = definition?.AllowedValues
        //        };
        //    }).ToList();

        //    return new ProductBySubCategoryResponseDto
        //    {
        //        Pid = entity.Id.ToString(),
        //        Cid = entity.CategoryId.ToString(),
        //        Scid = entity.SubCategoryId.ToString(),
        //        Name = entity.Name,
        //        ThumbnailUrl = entity.ThumbnailUrl,
        //        Cost = (double)entity.Price,
        //        Rating = entity.Rating,
        //        Reviews = entity.Reviews,
        //        CategoryType = entity.CategoryNameSnapshot,
        //        Quantity = 1,
        //        Limit = 1,
        //        Attributes = attributes
        //    };
        //}
    }
}
