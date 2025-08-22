
using Product.Application.Contracts;

namespace Product.Application.Services
{
    public interface ICatalogService
{
    Task<List<CatalogAttributeDto>> GetAttributesBySubCategoryIdAsync(int subCategoryId);
}

}