namespace Shared.Application.Common.Contracts
{
    public class BaseCatalogAttributeAllowedValueDto
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public int SortOrder { get; set; }
    }
}
