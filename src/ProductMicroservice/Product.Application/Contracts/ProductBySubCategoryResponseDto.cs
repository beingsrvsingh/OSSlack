
using Microsoft.AspNetCore.Mvc;
using Product.Domain.Entities;
using Shared.Application.Common.Contracts;
using System.Text.Json.Serialization;

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
                            Icon = attr.Icon
                        };
                    })
                    .ToList()
            })
            .Where(group => group.Attributes.Any())
            .ToList();

            return new ProductSummaryResponseDto
            {
                Pid = entity.Id.ToString(),
                //Cid = entity.CategoryId.ToString(),
                //Scid = entity.SubCategoryId.ToString(),
                Name = entity.Name,
                ThumbnailUrl = entity.ThumbnailUrl,
                Rating = entity.Rating,
                Reviews = entity.Reviews,
                //CategoryType = entity.CategoryNameSnapshot,
                //Quantity = 1,
                //Limit = 1,
                Images = entity.Media.Select(img => new ProductImageDto
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

        [JsonPropertyName("product_images")]
        public List<string>? ProductImages { get; set; }

        [JsonPropertyName("variants")]
        public List<VariantResponseDto>? Variants { get; set; }

        public static List<ProductBySubCategoryResponseDto> FromEntityList(IEnumerable<ProductMaster> products, IEnumerable<CatalogAttributeDto> catalogAttributeGroups, bool isSummary = false)
        {
            return products.Select(p => FromEntity(p)).ToList();
        }

        public static ProductBySubCategoryResponseDto FromEntity(ProductMaster entity)
        {
            return new ProductBySubCategoryResponseDto
            {
                Pid = $"P{entity.Id:D4}",
                Name = entity.Name,
                ThumbnailUrl = entity.ThumbnailUrl,
                Rating = entity.Rating,
                Reviews = entity.Reviews,
                CategoryId = entity.CategoryId.ToString() ?? string.Empty,
                SubCategoryId = entity.SubCategoryId.ToString() ?? string.Empty,
                IsActive = entity.IsActive,
                IsTrending = entity.IsTrending ?? false,
                IsFeatured = entity.IsFeatured ?? false,
                Tags = new List<string> { entity.CategoryNameSnapshot ?? "", entity.SubCategoryNameSnapshot ?? "" },
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Currency = entity?.Price?.Currency ?? "INR",

                // Collect all variant-level attributes and group them
                Attributes = entity?.VariantMasters?
                .SelectMany(v => v.Attributes ?? new List<ProductAttributeValue>())
                .Where(a => !string.IsNullOrEmpty(a.AttributeKey) && !string.IsNullOrEmpty(a.Value))
                .GroupBy(a => a.AttributeKey!)
                .Select(g => new ProductAttributeDto
                {
                    Key = g.Key,
                    Label = g.First().AttributeLabel ?? g.Key,
                    DataType = g.First().AttributeDataTypeId?.ToString() ?? "String",
                    Values = g.Select(a => a.Value!).Distinct().ToList(),
                    Icon = null
                })
                .ToList() ?? new List<ProductAttributeDto>(),

                ProductImages = entity?.Media?.Select(i => i.ImageUrl).ToList() ?? new(),

                Variants = entity?.VariantMasters?.Select(v => new VariantResponseDto
                {
                    Sku = $"SKU-{v.Id:D4}",
                    Name = v.Name,
                    Price = v.Price.Amount,
                    MRP = v.Price.Mrp,
                    Quantity = v.StockQuantity ?? 0,
                    IsDefault = v.IsDefault,
                    Attributes = v.Attributes?.Where(a => !string.IsNullOrEmpty(a.AttributeKey) && !string.IsNullOrEmpty(a.Value))
                                .ToDictionary(a => a.AttributeKey!, a => a.Value!) ?? new Dictionary<string, string>(),
                    VariantImages = v.Media?.Select(i => i.ImageUrl).ToList() ?? new(),
                }).ToList() ?? new()
            };
        }

    }

}
