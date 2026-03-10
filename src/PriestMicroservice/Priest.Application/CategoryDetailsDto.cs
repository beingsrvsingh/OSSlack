namespace Priest.Application
{
    public class CategoryDetailsDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; } = string.Empty;
    }
}
