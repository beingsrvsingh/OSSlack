
using System.Text.Json.Serialization;
using Product.Domain.Entities;

namespace Product.Application.Contracts
{
    public class ProductBySubCategoryResponseDto
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

        [JsonPropertyName("attributes")]
        public List<ProductAttributeDto>? Attributes { get; set; }

        public static ProductBySubCategoryResponseDto FromEntity(ProductMaster entity, List<CatalogAttributeDto> catalogAttributes)
        {
            return new ProductBySubCategoryResponseDto
            {
                Pid = entity.Id.ToString(),
                Cid = "", // Set based on your logic
                Scid = entity.SubCategoryId.ToString(),
                Name = entity.Name,
                Url = entity.ImageUrl,
                Cost = (double)entity.Price,
                Rating = 0,  // Map if rating exists
                Reviews = 0, // Map if reviews count exists
                CategoryType = entity.CategoryNameSnapshot,
                Quantity = 1,
                Limit = 1,
                Attributes = entity.AttributeValues?.Select(attrVal =>
                {
                    var definition = catalogAttributes.FirstOrDefault(a => a.Key == attrVal.AttributeKey);
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

        public static List<ProductBySubCategoryResponseDto> FromEntityList(IEnumerable<ProductMaster> products, List<CatalogAttributeDto> catalogAttributes)
        {
            return products.Select(p => FromEntity(p, catalogAttributes)).ToList();
        }

    }
}