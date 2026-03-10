namespace Priest.Application
{
    public class CategoryDetailsResponseDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;        
        public List<SubCategoryDto> SubCategories { get; set; } = new();
    }

    public class SubCategoryDto
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; } = string.Empty;
        public string ThumbnailUril { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
