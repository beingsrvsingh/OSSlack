namespace Pooja.Application.Contracts
{
    public class CatalogAttributeGroupDto
    {
        public string GroupName { get; set; } = string.Empty;
        public List<CatalogAttributeDto> Attributes { get; set; } = new List<CatalogAttributeDto>();
    }
}
