using Microsoft.EntityFrameworkCore;
using Product.Domain.Core.Repository;
using Product.Domain.Entities;
using Product.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Product.Infrastructure.Repositories
{
    public class ProductAttributeRepository : Repository<ProductAttributeValue>, IProductAttributeRepository
    {
        private readonly ProductDbContext _context;

        public ProductAttributeRepository(ProductDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductAttributeValue>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductAttributeValues
                .Where(p => p.ProductMasterId == productId)
                .OrderBy(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}