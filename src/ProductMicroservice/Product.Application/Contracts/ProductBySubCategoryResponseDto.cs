
using System.Text.Json.Serialization;
using Product.Domain.Entities;

namespace Product.Application.Contracts
{
    public class ProductSummaryResponseDto : BaseProductResponseDto
    {
        [JsonPropertyName("attributes")]
        public List<ProductAttributeGroupDto>? Attributes { get; set; }

        public static ProductSummaryResponseDto FromGroupedAttributeEntity(
        ProductMaster entity,
        IEnumerable<CatalogAttributeGroupDto> catalogAttributeGroups,
        bool isSummary = true)
        {
            // Flatten and index by key
            var attributeDict = catalogAttributeGroups
                .SelectMany(g => g.Attributes)
                .ToDictionary(attr => attr.Key, attr => attr);

            var attributeValues = entity.AttributeValues ?? new List<ProductAttributeValue>();

            // Group attributes based on catalog groups
            var groupedAttributes = catalogAttributeGroups.Select(group => new ProductAttributeGroupDto
            {
                GroupName = group.GroupName,
                Attributes = group.Attributes
                    .Where(attr => attributeValues.Any(val => val.AttributeKey == attr.Key))
                    .Select(attr =>
                    {
                        var attrVal = attributeValues.First(val => val.AttributeKey == attr.Key);
                        return new ProductAttributeDto
                        {
                            Key = attrVal.AttributeKey ?? "",
                            Label = attrVal.AttributeLabel ?? attrVal.AttributeKey ?? "",
                            Value = attrVal.Value,
                            DataType = attr.DataType ?? "String",
                            Icon = attr.Icon,
                            AllowedValues = attr.AllowedValues
                        };
                    })
                    .ToList()
            })
            // Only include groups with at least one matching attribute
            .Where(group => group.Attributes.Any())
            .ToList();

            return new ProductSummaryResponseDto
            {
                Pid = entity.Id.ToString(),
                Cid = "", // populate if available
                Scid = entity.SubCategoryId.ToString(),
                Name = entity.Name,
                Url = entity.ImageUrl,
                Cost = (double)entity.Price,
                Rating = 0,
                Reviews = 0,
                CategoryType = entity.CategoryNameSnapshot,
                Quantity = 1,
                Limit = 1,
                Attributes = groupedAttributes
            };
        }

        public static List<ProductSummaryResponseDto> FromEntityList(IEnumerable<ProductMaster> products, IEnumerable<CatalogAttributeGroupDto> catalogAttributeGroups, bool isSummary = false)
        {
            return products.Select(p => FromGroupedAttributeEntity(p, catalogAttributeGroups, isSummary)).ToList();
        }
    }

    public class ProductBySubCategoryResponseDto : BaseProductResponseDto
    {
        [JsonPropertyName("attributes")]
        public List<ProductAttributeDto>? Attributes { get; set; }

        public static ProductBySubCategoryResponseDto FromEntity(ProductMaster entity,
        IEnumerable<CatalogAttributeGroupDto> catalogAttributeGroups,
        bool isSummary = false)
        {
            var attributeDict = catalogAttributeGroups
                .SelectMany(g => g.Attributes)
                .ToDictionary(attr => attr.Key, attr => attr);

            var attributeValues = entity.AttributeValues ?? new List<ProductAttributeValue>();

            // Conditionally filter attribute values if not summary
            if (!isSummary)
            {
                attributeValues = attributeValues
                    .Where(attrVal => attrVal.AttributeKey != null && attributeDict.ContainsKey(attrVal.AttributeKey))
                    .ToList();
            }

            return new ProductBySubCategoryResponseDto
            {
                Pid = entity.Id.ToString(),
                Cid = "", // Populate if available
                Scid = entity.SubCategoryId.ToString(),
                Name = entity.Name,
                Url = entity.ImageUrl,
                Cost = (double)entity.Price,
                Rating = 0,  // Map if rating exists
                Reviews = 0, // Map if reviews exist
                CategoryType = entity.CategoryNameSnapshot,
                Quantity = 1,
                Limit = 1,
                Attributes = attributeValues.Select(attrVal =>
                {
                    attributeDict.TryGetValue(attrVal.AttributeKey ?? "", out var definition);
                    return new ProductAttributeDto
                    {
                        Key = attrVal.AttributeKey ?? "",
                        Label = attrVal.AttributeLabel ?? attrVal.AttributeKey ?? "",
                        Value = attrVal.Value,
                        DataType = definition?.DataType ?? "String",
                        Icon = definition?.Icon,
                        AllowedValues = definition?.AllowedValues
                    };
                }).ToList()
            };
        }

        public static List<ProductBySubCategoryResponseDto> FromEntityList(IEnumerable<ProductMaster> products, IEnumerable<CatalogAttributeGroupDto> catalogAttributeGroups, bool isSummary = false)
        {
            return products.Select(p => FromEntity(p, catalogAttributeGroups, isSummary)).ToList();
        }

    }

    public partial class BaseProductResponseDto
    {
        [JsonPropertyName("pid")]
        public string Pid { get; set; } = null!;

        [JsonPropertyName("cid")]
        public string Cid { get; set; } = string.Empty;

        [JsonPropertyName("scid")]
        public string Scid { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("cost")]
        public double Cost { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; } = 0;

        [JsonPropertyName("reviews")]
        public int Reviews { get; set; } = 0;

        [JsonPropertyName("categorytype")]
        public string? CategoryType { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; } = 1;
        [JsonPropertyName("limit")]
        public int Limit { get; set; } = 1;
    }
}
