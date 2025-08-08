using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Domain.Repository;
using Product.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Product.Infrastructure.Repositories
{
    public class ProductRepository : Repository<ProductMaster>, IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductMaster?> GetProductWithVariantsAsync(int productId)
        {
            return await _context.ProductMasters
                .Include(p => p.VariantMasters) // Assuming navigation property
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<IEnumerable<ProductRegionPriceMaster>> GetRegionPricesAsync(int productId)
        {
            return await _context.ProductRegionPriceMasters
                .Where(p => p.ProductId == productId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductVariantMaster>> GetVariantsAsync(int productId)
        {
            return await _context.ProductVariantMasters
                .Where(v => v.ProductId == productId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductAttributeMaster>> GetAttributesAsync(int productId)
        {
            return await _context.ProductAttributeMasters
                .Where(a => a.ProductId == productId)
                .ToListAsync();
        }

        public async Task<IEnumerable<LocalizedProductInfoMaster>> GetLocalizedInfoAsync(int productId)
        {
            return await _context.LocalizedProductInfoMasters
                .Where(l => l.ProductId == productId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductTagMaster>> GetTagsAsync(int productId)
        {
            return await _context.ProductTagMasters
                .Where(t => t.ProductId == productId)
                .ToListAsync();
        }

        public async Task<ProductSEOInfoMaster?> GetSEOInfoAsync(int productId)
        {
            return await _context.ProductSEOInfoMasters
                .FirstOrDefaultAsync(s => s.ProductId == productId);
        }
    }

}