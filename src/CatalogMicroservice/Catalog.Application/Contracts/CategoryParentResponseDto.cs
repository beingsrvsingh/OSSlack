using System.Text.Json.Serialization;

namespace Catalog.Application.Contracts
{
    public class CategoryParentResponseDto
    {
        [JsonPropertyName("category_name")]
        public required string CategoryName { get; set; }        

        [JsonPropertyName("subcategories")]        
        public List<SubCategoryParentResponseDto> Subcategories { get; set; } = new();
    }
}
