namespace Catalog.Domain.Entities
{
    public class ParentCategoryRaw
    {
        public string CategoryName { get; set; }
        public string ResourceType { get; set; }
        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
    }
}
