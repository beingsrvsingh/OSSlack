using System.Linq.Expressions;
using Product.Domain.Entities;
using Shared.Domain.Repository;

namespace Product.Domain.Repository
{
    public interface IProductRepository : IRepository<ProductMaster>
    {
        Task<ProductMaster?> GetProductWithVariantsAsync(int productId);
        Task<IEnumerable<ProductRegionPriceMaster>> GetRegionPricesAsync(int productId);
        Task<IEnumerable<ProductVariantMaster>> GetVariantsAsync(int productId);
        Task<IEnumerable<LocalizedProductInfoMaster>> GetLocalizedInfoAsync(int productId);
        Task<IEnumerable<ProductTagMaster>> GetTagsAsync(int productId);
        Task<ProductSEOInfoMaster?> GetSEOInfoAsync(int productId);
        Task<List<ProductMaster>> GetAsync(Expression<Func<ProductMaster, bool>> predicate, Func<IQueryable<ProductMaster>, IQueryable<ProductMaster>>? include = null);
        Task<ProductMaster?> GetSingleAsync(Expression<Func<ProductMaster, bool>> predicate, Func<IQueryable<ProductMaster>, IQueryable<ProductMaster>>? include = null);
        Task<List<ProductFilterRawResult>> GetFilteredProductsRawAsync(List<int>attributeIds, int pageNumber,
        int pageSize,
        string? sortBy = null,
        bool sortDescending = false);
    }
}