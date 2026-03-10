namespace Shared.Contracts.Contracts
{
    public class CategoryDetailsRequest
    {
        public List<int> CategoryIds { get; set; } = new();
        public List<int> SubCategoryIds { get; set; } = new();
    }
}
