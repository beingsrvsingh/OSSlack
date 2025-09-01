
using System.Text.Json.Serialization;
using Product.Domain.Entities;

namespace Product.Application.Contracts
{
    public class ProductSummaryResponseDto : BaseProductResponseDto
    {
        [JsonPropertyName("attributes")]
        public List<ProductAttributeGroupDto>? Attributes { get; set; }

        [JsonPropertyName("images")]
        public List<ProductImageDto> Images { get; set; } = new();

        public static List<ProductSummaryResponseDto> FromEntityList(IEnumerable<ProductMaster> products, IEnumerable<CatalogAttributeGroupDto> catalogAttributeGroups, bool isSummary = false)
        {
            return products.Select(p => FromSummaryEntity(p, catalogAttributeGroups)).ToList();
        }

        public static ProductSummaryResponseDto FromSummaryEntity(
        ProductMaster entity,
        IEnumerable<CatalogAttributeGroupDto> catalogAttributeGroups)
        {
            // Flatten and index by key
            var attributeDict = catalogAttributeGroups
                .SelectMany(g => g.Attributes)
                .ToDictionary(attr => attr.Key, attr => attr);

            var attributeValues = entity.AttributeValues ?? new List<ProductAttributeValue>();

            // Group attribute values by key to support multiple values (e.g., multiple colors)
            var groupedAttributeValues = attributeValues
                .Where(val => !string.IsNullOrEmpty(val.AttributeKey))
                .GroupBy(val => val.AttributeKey!)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Group attributes based on catalog attribute group
            var groupedAttributes = catalogAttributeGroups.Select(group => new ProductAttributeGroupDto
            {
                GroupName = group.GroupName,
                Attributes = group.Attributes
                    .Where(attr => groupedAttributeValues.ContainsKey(attr.Key))
                    .Select(attr =>
                    {
                        var values = groupedAttributeValues[attr.Key];
                        var firstVal = values.First();

                        return new ProductAttributeDto
                        {
                            Key = firstVal.AttributeKey!,
                            Label = firstVal.AttributeLabel ?? firstVal.AttributeKey!,
                            Values = values.Select(v => v.Value).ToList(),
                            DataType = attr.DataType ?? "String",
                            Icon = attr.Icon,
                            AllowedValues = attr.AllowedValues
                        };
                    })
                    .ToList()
            })
            .Where(group => group.Attributes.Any())
            .ToList();

            return new ProductSummaryResponseDto
            {
                Pid = entity.Id.ToString(),
                Cid = entity.CategoryId.ToString(),
                Scid = entity.SubCategoryId.ToString(),
                Name = entity.Name,
                ThumbnailUrl = entity.ThumbnailUrl,
                Cost = (double)entity.Price,
                Rating = entity.Rating,
                Reviews = entity.Reviews,
                CategoryType = entity.CategoryNameSnapshot,
                Quantity = 1,
                Limit = 1,
                Images = entity.Images.Select(img => new ProductImageDto
                {
                    ImageUrl = img.ImageUrl
                }).ToList(),
                Attributes = groupedAttributes
            };
        }
    }

    public class ProductBySubCategoryResponseDto : BaseProductResponseDto
    {
        [JsonPropertyName("attributes")]
        public List<ProductAttributeDto>? Attributes { get; set; }

        public static List<ProductBySubCategoryResponseDto> FromEntityList(IEnumerable<ProductMaster> products, IEnumerable<CatalogAttributeDto> catalogAttributeGroups, bool isSummary = false)
        {
            return products.Select(p => FromEntity(p, catalogAttributeGroups, isSummary)).ToList();
        }

        public static ProductBySubCategoryResponseDto FromEntity(
        ProductMaster entity,
        IEnumerable<CatalogAttributeDto> catalogAttributes,
        bool isSummary = false)
        {
            // Build dictionary from attribute definitions keyed by attribute key
            var attributeDict = catalogAttributes
                .ToDictionary(attr => attr.Key, attr => attr);

            var attributeValues = entity.AttributeValues ?? new List<ProductAttributeValue>();

            // If not summary view, only include values with matching definitions
            if (!isSummary)
            {
                attributeValues = attributeValues
                    .Where(attrVal => attrVal.AttributeKey != null && attributeDict.ContainsKey(attrVal.AttributeKey))
                    .ToList();
            }

            // Group values by attribute key
            var groupedAttributeValues = attributeValues
                .Where(val => !string.IsNullOrEmpty(val.AttributeKey))
                .GroupBy(val => val.AttributeKey!)
                .ToList();

            // Build ProductAttributeDto list
            var attributes = groupedAttributeValues.Select(group =>
            {
                var firstVal = group.First();
                attributeDict.TryGetValue(group.Key, out var definition);

                return new ProductAttributeDto
                {
                    Key = firstVal.AttributeKey!,
                    Label = firstVal.AttributeLabel ?? firstVal.AttributeKey!,
                    Values = group.Select(v => v.Value).Where(v => !string.IsNullOrWhiteSpace(v)).ToList(),
                    DataType = definition?.DataType ?? "String",
                    Icon = definition?.Icon,
                    AllowedValues = definition?.AllowedValues
                };
            }).ToList();

            return new ProductBySubCategoryResponseDto
            {
                Pid = entity.Id.ToString(),
                Cid = entity.CategoryId.ToString(),
                Scid = entity.SubCategoryId.ToString(),
                Name = entity.Name,
                ThumbnailUrl = entity.ThumbnailUrl,
                Cost = (double)entity.Price,
                Rating = entity.Rating,
                Reviews = entity.Reviews,
                CategoryType = entity.CategoryNameSnapshot,
                Quantity = 1,
                Limit = 1,
                Attributes = attributes
            };
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

        [JsonPropertyName("thumbnail_url")]
        public string? ThumbnailUrl { get; set; }

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
