
using System.Text.Json.Serialization;
using Product.Domain.Entities;

namespace Product.Application.Contracts
{
    public class ProductBySubCategoryResponseDto
    {
        [JsonPropertyName("pid")]
        public string Pid { get; set; } = null!;

        [JsonPropertyName("cid")]
        public string Cid { get; set; } = null!;

        [JsonPropertyName("scid")]
        public string Scid { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("cost")]
        public double Cost { get; set; }

        [JsonPropertyName("reviews")]
        public int Reviews { get; set; } = 0;

        [JsonPropertyName("rating")]
        public double Rating { get; set; } = 0;

        [JsonPropertyName("discount")]
        public string? Discount { get; set; }

        [JsonPropertyName("samagricost")]
        public double? Samagricost { get; set; }

        [JsonPropertyName("isselected")]
        public bool? IsSelected { get; set; }

        [JsonPropertyName("limit")]
        public int? Limit { get; set; }

        [JsonPropertyName("categorytype")]
        public string? CategoryType { get; set; }

        [JsonPropertyName("includes")]
        public List<string>? Includes { get; set; }

        [JsonPropertyName("preparationtime")]
        public string? PreparationTime { get; set; }

        public static ProductBySubCategoryResponseDto FromEntity(ProductMaster entity)
        {
            return new ProductBySubCategoryResponseDto
            {
                Pid = entity.Id.ToString(),
                Cid = "", // Set this based on your logic or include in entity
                Scid = entity.SubCategoryId.ToString(),
                Name = entity.Name,
                Description = entity.Description,
                Url = entity.ImageUrl,
                Location = null, // Add if you have location info in entity
                Cost = (double)entity.Price,
                Reviews = 0,  // If you have reviews count, map here
                Rating = 0,   // If rating exists, map here
                Discount = null, // Map if you have
                Samagricost = null,
                IsSelected = null,
                Limit = null,
                CategoryType = entity.CategoryNameSnapshot,
                Includes = null, // Map if available
                PreparationTime = null, // Map if available
            };
        }

        public static List<ProductBySubCategoryResponseDto> FromEntityList(IEnumerable<ProductMaster> products)
        {
            return products.Select(p => FromEntity(p)).ToList();
        }

    }
}